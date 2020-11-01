using Model;
using System;
using System.Collections.Generic;
using System.Timers;

public delegate void OnDriversChanged(object sender, DriversChangedEventArgs e);
public delegate void OnRaceFinished(object sender, RaceFinishedEventArgs e);
public delegate void OnStartNextRace(object sender, EventArgs e);

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private readonly Stack<IParticipant> FinishedParticipants;
        private readonly int DistancePerSection;
        private readonly int RoundsPerRace;
        private readonly Timer Timer;
        private readonly Random _random;
        private readonly Dictionary<Section, SectionData> _positions;
        private readonly Dictionary<IParticipant, int> _rounds;
        private readonly Dictionary<IParticipant, long> _sectionTimes;
        private readonly Dictionary<IParticipant, TimeSpan> _timeBroken;
        private readonly Dictionary<IParticipant, int> _startPerformance;

        public event OnDriversChanged DriversChanged;
        public event OnRaceFinished RaceFinished;
        public event OnStartNextRace StartNextRace;

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            FinishedParticipants = new Stack<IParticipant>();
            DistancePerSection = 100;
            RoundsPerRace = 1;
            Timer = new Timer(500);

            _random = new Random(DateTime.Now.Millisecond);
            _positions = new Dictionary<Section, SectionData>();
            _rounds = new Dictionary<IParticipant, int>();
            _sectionTimes = new Dictionary<IParticipant, long>();
            _timeBroken = new Dictionary<IParticipant, TimeSpan>();
            _startPerformance = new Dictionary<IParticipant, int>();

            Timer.Elapsed += OnTimedEvent;

            RandomizeEquipment();
            SetStartPositionParticipants();
            FillRoundsDictionary();
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

        private void Start()
        {
            foreach (IParticipant participant in Participants)
            {
                _sectionTimes.Add(participant, 0);
                _timeBroken.Add(participant, new TimeSpan(StartTime.Ticks));
                _startPerformance.Add(participant, participant.Equipment.Performance);
            }

            Timer.Start();
            StartTime = DateTime.Now;
        }

        private void Stop()
        {
            Timer.Stop();
            SaveParticipantsTimeBroken();
            SaveParticipantsPerformance();

            Timer.Elapsed -= OnTimedEvent;

            RaceFinished?.Invoke(this, new RaceFinishedEventArgs(FinishedParticipants));
            StartNextRace?.Invoke(this, EventArgs.Empty);
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            foreach (Section section in Track.Sections)
            {
                SectionData sectionData = GetSectionData(section);

                foreach (IParticipant participant in Participants)
                {
                    if (section.SectionType != SectionTypes.StartGrid && section.SectionType != SectionTypes.Finish)
                        RandomizeEquipmentIsBroken();

                    int speed = participant.Equipment.Performance * participant.Equipment.Speed;

                    if (sectionData.Left != null)
                    {
                        if (sectionData.Left.Equals(participant))
                        {
                            if (!participant.Equipment.IsBroken)
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

                                    if (section.SectionType == SectionTypes.Finish)
                                        _rounds[participant]++;

                                    if (_rounds[participant] > RoundsPerRace)
                                    {
                                        sectionData.Left = null;
                                        sectionData.DistanceLeft = 0;

                                        nextSectionData.Left = null;
                                        nextSectionData.DistanceLeft = 0;

                                        FinishedParticipants.Push(participant);
                                    }

                                    SaveParticipantSectionTime(section, participant, e);
                                }
                            }
                            else
                            {
                                SetTimeBroken(participant, e);
                            }
                        }
                    }

                    if (sectionData.Right != null)
                    {
                        if (sectionData.Right.Equals(participant))
                        {
                            if (!participant.Equipment.IsBroken)
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

                                    if (section.SectionType == SectionTypes.Finish)
                                        _rounds[participant]++;

                                    if (_rounds[participant] > RoundsPerRace)
                                    {
                                        sectionData.Right = null;
                                        sectionData.DistanceRight = 0;

                                        nextSectionData.Right = null;
                                        nextSectionData.DistanceRight = 0;

                                        FinishedParticipants.Push(participant);
                                    }

                                    SaveParticipantSectionTime(section, participant, e);
                                }
                            }
                            else
                            {
                                SetTimeBroken(participant, e);
                            }
                        }
                    }

                }
            }

            DriversChanged?.Invoke(this, new DriversChangedEventArgs(Track));

            if (FinishedParticipants.Count == Participants.Count)
            {
                Stop();
            }
        }

        private void RandomizeEquipment()
        {
            List<int> givenPerformance = new List<int>();

            while (givenPerformance.Count < Participants.Count)
            {
                foreach (IParticipant participant in Participants)
                {
                    participant.Equipment.Quality = _random.Next(1, 10);

                    int performance = _random.Next(6, 8);

                    if (!givenPerformance.Contains(performance))
                    {
                        participant.Equipment.Performance = performance;
                        givenPerformance.Add(performance);
                    }
                }
            }
        }

        private void RandomizeEquipmentIsBroken()
        {
            foreach (IParticipant participant in Participants)
            {
                if (participant.Equipment.IsBroken)
                {
                    if (_random.Next(200) == 100)
                    {
                        participant.Equipment.IsBroken = false;

                        if (participant.Equipment.Performance > 3)
                            participant.Equipment.Performance--;
                    }
                }
                else if (_random.Next(1500) == 750)
                {
                    participant.Equipment.IsBroken = true;
                }
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

        private void SetTimeBroken(IParticipant participant, ElapsedEventArgs e)
        {
            _timeBroken[participant] = new TimeSpan(e.SignalTime.Ticks - StartTime.Ticks);
        }

        private void FillRoundsDictionary()
        {
            foreach (IParticipant participant in Participants)
                _rounds.Add(participant, 0);
        }

        private void SaveParticipantSectionTime(Section section, IParticipant participant, ElapsedEventArgs e)
        {
            long ticksLastRound = _sectionTimes[participant];
            if (ticksLastRound == 0)
                ticksLastRound = StartTime.Ticks;

            TimeSpan timeSpan = new TimeSpan(e.SignalTime.Ticks - ticksLastRound);
            Data.Competition.ParticipantLapTime.Add(new DataParticipantLapTime(participant, section, timeSpan));
        }

        private void SaveParticipantsTimeBroken()
        {
            foreach (KeyValuePair<IParticipant, TimeSpan> kv in _timeBroken)
                Data.Competition.ParticipantTimeBroken.Add(new DataParticipantTimeBroken(kv.Key, kv.Value));
        }

        private void SaveParticipantsPerformance()
        {
            foreach (KeyValuePair<IParticipant, int> kv in _startPerformance)
                Data.Competition.ParticipantPerformanceBeforeAndAfter.Add(new DataParticipantPerformanceBeforeAndAfter(kv.Key, kv.Value, kv.Key.Equipment.Performance));
        }
    }
}
    