using Model;
using System;
using System.Collections.Generic;
using System.Timers;
using static Model.DriversChangedEventArgs;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private readonly int DistancePerSection;
        private readonly Timer Timer;
        private readonly Random _random;
        private readonly Dictionary<Section, SectionData> _positions;

        public event DriversChanged DriversChanged;

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            DistancePerSection = 100;
            Timer = new Timer(500);
            Timer.Elapsed += OnTimedEvent;
            _random = new Random(DateTime.Now.Millisecond);
            _positions = new Dictionary<Section, SectionData>();
            RandomizeEquipment();
            SetStartPositionParticipants();
            Start();
        }

        public SectionData GetSectionData(Section section)
        {
            if (!_positions.ContainsKey(section))
            {
                _positions.Add(section, new SectionData());
            }

            return _positions[section];
        }

        public void OnTimedEvent(object source, EventArgs eventArgs)
        {
            foreach (Section section in Track.Sections)
            {
                SectionData sectionData = GetSectionData(section);

                foreach (IParticipant participant in Participants)
                {
                    int speed = participant.Equipment.Performance * participant.Equipment.Speed;

                    if (sectionData.Left != null)
                    {
                        if (sectionData.Left.Equals(participant))
                        {
                            sectionData.DistanceLeft += speed;

                            if (sectionData.DistanceLeft >= DistancePerSection)
                            {
                                var next = Track.Sections.Find(section).Next;
                                Section nextSection = next != null ? next.Value : Track.Sections.First.Value;
                                SectionData nextSectionData = GetSectionData(nextSection);

                                nextSectionData.Left = participant;
                                nextSectionData.DistanceLeft = sectionData.DistanceLeft - DistancePerSection;

                                sectionData.Left = null;
                                sectionData.DistanceLeft = 0;

                                DriversChanged(new DriversChangedEventArgs(Track));
                            }
                        }
                    }

                    if (sectionData.Right != null)
                    {
                        if (sectionData.Right.Equals(participant))
                        {
                            sectionData.DistanceRight += speed;

                            if (sectionData.DistanceRight >= DistancePerSection)
                            {
                                var next = Track.Sections.Find(section).Next;
                                Section nextSection = next != null ? next.Value : Track.Sections.First.Value;
                                SectionData nextSectionData = GetSectionData(nextSection);

                                nextSectionData.Right = participant;
                                nextSectionData.DistanceRight = sectionData.DistanceRight - DistancePerSection;

                                sectionData.Right = null;
                                sectionData.DistanceRight = 0;

                                DriversChanged(new DriversChangedEventArgs(Track));
                            }
                        }
                    }
                }
            }
        }

        private void Start()
        {
            Timer.Start();
        }

        private void RandomizeEquipment()
        {
            foreach (IParticipant participant in Participants)
            {
                participant.Equipment.Quality = _random.Next(1, 5);
                participant.Equipment.Performance = _random.Next(1, 5);
            }
        }

        private void SetStartPositionParticipants()
        {
            foreach (Section section in Track.Sections)
            {
                SectionData sectionData = GetSectionData(section);

                if (section.SectionType == SectionTypes.StartGrid)
                {
                    sectionData.Left = Participants[0];
                    sectionData.DistanceLeft = 0;
                    sectionData.Right = Participants[1];
                    sectionData.DistanceRight = 0;
                }
            }
        }

    }
}
    