using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class DataParticipantPoints : IDataParticipant
    {
        public IParticipant Participant { get; set; }
        public int Points { get; set; }

        public DataParticipantPoints(IParticipant participant, int points)
        {
            Participant = participant;
            Points = points;
        }

        public void Add(List<IDataParticipant> list)
        {
            foreach (DataParticipantPoints item in list)
            {
                if (item.Participant.Equals(Participant))
                {
                    item.Points += Points;
                    return;
                }
            }

            list.Add(this);
        }

        public string GetBestParticipant(List<IDataParticipant> list)
        {
            DataParticipantPoints best = null;

            foreach (DataParticipantPoints item in list)
            {
                if (best != null)
                {
                    if (item.Points > best.Points)
                    {
                        best = item;
                    }
                }
                else
                {
                    best = item;
                }
            }

            return $"{best.Participant.Name} got the most points ({best.Points})";
        }

        public List<IDataParticipant> GetParticipantsOrderedByBest(List<IDataParticipant> list)
        {
            return list.OrderByDescending(x => x.Participant.Points).ToList();
        }
    }
}
