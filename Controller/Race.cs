using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private readonly Random _random;
        private readonly Dictionary<Section, SectionData> _positions;

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            _positions = new Dictionary<Section, SectionData>();
            RandomizeEquipment();
            SetStartPositionParticipants();
        }

        public SectionData GetSectionData(Section section)
        {
            if (!_positions.ContainsKey(section))
            {
                _positions.Add(section, new SectionData());
            }

            return _positions[section];
        }

        private void RandomizeEquipment()
        {
            foreach (IParticipant participant in Participants)
            {
                participant.IEquipment.Quality = _random.Next(1, 10);
                participant.IEquipment.Performance = _random.Next(1, 10);
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
    