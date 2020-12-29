using System.Collections.Generic;

namespace Model
{
    public class DataParticipantPerformanceBeforeAndAfter : IDataParticipant
    {
        public IParticipant Participant { get; set; }
        public int PerformanceBefore { get; set; }
        public int PerformanceAfter { get; set; }

        public DataParticipantPerformanceBeforeAndAfter(IParticipant participant ,int performanceBefore, int performanceAfter)
        {
            Participant = participant;
            PerformanceBefore = performanceBefore;
            PerformanceAfter = performanceAfter;
        }

        public void Add(List<IDataParticipant> list)
        {
            foreach (DataParticipantPerformanceBeforeAndAfter item in list)
            {
                if (item.Participant.Equals(Participant))
                {
                    if (item.PerformanceAfter > PerformanceAfter)
                    {
                        item.PerformanceBefore = PerformanceAfter;
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

        public List<IDataParticipant> GetParticipantsOrderedByBest(List<IDataParticipant> list)
        {
            throw new System.NotImplementedException();
        }
    }
}
