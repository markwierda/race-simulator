using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class DataParticipantTimeBroken : IDataParticipant
    {
        public IParticipant Participant { get; set; }
        public TimeSpan TimeBroken { get; set; }
        public string Track { get; set; }

        public DataParticipantTimeBroken(IParticipant participant, TimeSpan timeBroken, string track)
        {
            Participant = participant;
            TimeBroken = timeBroken;
            Track = track;
        }

        public void Add(List<IDataParticipant> list)
        {
            foreach (DataParticipantTimeBroken item in list)
            {
                if (item.Participant.Equals(Participant) && Track.Equals(item.Track))
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

        public List<IDataParticipant> GetParticipantsOrderedByBest(List<IDataParticipant> list, string track)
        {
            List<DataParticipantTimeBroken> newList = new List<DataParticipantTimeBroken>();

            foreach (DataParticipantTimeBroken item in list)
            {
                if (item.Track.Equals(track))
                {
                    newList.Add(item);
                }
            }

            newList.OrderBy(x => x.TimeBroken);

            list = new List<IDataParticipant>();
            foreach (IDataParticipant item in newList)
            {
                list.Add(item);
            }

            return list;
        }
    }
}