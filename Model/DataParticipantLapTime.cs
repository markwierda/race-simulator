using System;
using System.Collections.Generic;

namespace Model
{
    public class DataParticipantLapTime : IDataParticipant
    {
        public IParticipant Participant { get; set; }
        public Section Section { get; set; }
        public TimeSpan SectionTime { get; set; }

        public DataParticipantLapTime(IParticipant participant, Section section, TimeSpan sectionTime)
        {
            Participant = participant;
            Section = section;
            SectionTime = sectionTime;
        }

        public void Add(List<IDataParticipant> list)
        {
            foreach (DataParticipantLapTime item in list)
            {
                if (item.Participant.Equals(Participant) && item.Section.Equals(Section))
                {
                    item.SectionTime = SectionTime;
                    return;
                }
            }

            list.Add(this);
        }

        public string GetBestParticipant(List<IDataParticipant> list)
        {
            Dictionary<IParticipant, TimeSpan> laptimes = GetLaptimes(list);
            IParticipant best = null;

            foreach (KeyValuePair<IParticipant, TimeSpan> laptime in laptimes)
            {
                if (best != null)
                {
                    if (laptime.Value < laptimes[best])
                    {
                        best = laptime.Key;
                    }
                }
                else
                {
                    best = laptime.Key;
                }
            }

            return $"{best.Name} got the best laptime ({laptimes[best]})";
        }

        private Dictionary<IParticipant, TimeSpan> GetLaptimes(List<IDataParticipant> list)
        {
            Dictionary<IParticipant, TimeSpan> laptimes = new Dictionary<IParticipant, TimeSpan>();

            foreach (DataParticipantLapTime item in list)
            {
                if (laptimes.ContainsKey(item.Participant))
                {
                    laptimes[item.Participant] += item.SectionTime;
                }
                else
                {
                    laptimes.Add(item.Participant, item.SectionTime);
                }
            }

            return laptimes;
        }

        public List<IDataParticipant> GetParticipantsOrderedByBest(List<IDataParticipant> list)
        {
            throw new NotImplementedException();
        }
    }
}