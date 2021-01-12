using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class DataParticipantPerformanceBeforeAndAfter : IDataParticipant
    {
        public IParticipant Participant { get; set; }
        public int PerformanceBefore { get; set; }
        public int PerformanceAfter { get; set; }
        public string Track { get; set; }

        public DataParticipantPerformanceBeforeAndAfter(IParticipant participant, int performanceBefore, int performanceAfter, string track)
        {
            Participant = participant;
            PerformanceBefore = performanceBefore;
            PerformanceAfter = performanceAfter;
            Track = track;
        }

        public void Add(List<IDataParticipant> list)
        {
            foreach (DataParticipantPerformanceBeforeAndAfter item in list)
            {
                if (item.Participant.Equals(Participant) && Track.Equals(item.Track))
                {
                    if (item.PerformanceAfter > PerformanceAfter)
                    {
                        item.PerformanceAfter = PerformanceAfter;
                    }

                    return;
                }
            }

            list.Add(this);
        }

        public string GetBestParticipant(List<IDataParticipant> list)
        {
            DataParticipantPerformanceBeforeAndAfter best = null;

            foreach (DataParticipantPerformanceBeforeAndAfter item in list)
            {
                if (best != null)
                {
                    if (item.PerformanceAfter > best.PerformanceAfter)
                    {
                        best = item;
                    }
                }
                else
                {
                    best = item;
                }
            }

            return $"{best.Participant.Name} got the best PerformanceAfter ({best.PerformanceAfter})";
        }

        public List<IDataParticipant> GetParticipantsOrderedByBest(List<IDataParticipant> list, string track)
        {
            List<DataParticipantPerformanceBeforeAndAfter> newList = new List<DataParticipantPerformanceBeforeAndAfter>();

            foreach (DataParticipantPerformanceBeforeAndAfter item in list)
            {
                if (item.Track.Equals(track))
                {
                    newList.Add(item);
                }
            }

            newList.OrderBy(x => x.PerformanceAfter);

            list = new List<IDataParticipant>();
            foreach (IDataParticipant item in newList)
            {
                list.Add(item);
            }

            return list;
        }
    }
}