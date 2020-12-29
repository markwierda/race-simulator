using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class DataParticipantTimeBroken : IDataParticipant
    {
        public IParticipant Participant { get; set; }
        public TimeSpan TimeBroken { get; set; }

        public DataParticipantTimeBroken(IParticipant participant, TimeSpan timeBroken)
        {
            Participant = participant;
            TimeBroken = timeBroken;
        }

        public void Add(List<IDataParticipant> list)
        {
            foreach (DataParticipantTimeBroken item in list)
            {
                if (item.Participant.Equals(Participant))
                {
                    item.TimeBroken += TimeBroken;
                    return;
                }
            }

            list.Add(this);
        }

        public string GetBestParticipant(List<IDataParticipant> list)
        {
            DataParticipantTimeBroken best = null;

            foreach (DataParticipantTimeBroken item in list)
            {
                if (best != null)
                {
                    if (item.TimeBroken > best.TimeBroken)
                    {
                        best = item;
                    }
                }
                else
                {
                    if (item.TimeBroken > TimeSpan.Zero)
                    {
                        best = item;
                    }
                }
            }

            return best != null ? $"{best.Participant.Name} has been broken for the longest time ({best.TimeBroken})" : "None have been broken";
        }

        public List<IDataParticipant> GetParticipantsOrderedByBest(List<IDataParticipant> list)
        {
            List<DataParticipantTimeBroken> newList = new List<DataParticipantTimeBroken>();

            foreach (DataParticipantTimeBroken item in list)
            {
                newList.Add(item);
            }

            newList.OrderByDescending(x => x.TimeBroken);

            list = new List<IDataParticipant>();
            foreach (IDataParticipant item in newList)
            {
                list.Add(item);
            }

            return list;
        }
    }
}
