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
        private Random _random;
        private Dictionary<Section, SectionData> _positions;

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            RandomizeEquipment();
        }

        public SectionData GetSectionData(Section section)
        {
            if (!_positions.ContainsKey(section))
            {
                _positions.Add(section, new SectionData());
            }

            return _positions[section];
        }

        public void RandomizeEquipment()
        {
            Random random = new Random();

            foreach (IParticipant participant in Participants)
            {
                participant.IEquipment.Quality = random.Next(1, 10);
                participant.IEquipment.Performance = random.Next(1, 10);
            }
        }

    }
}
