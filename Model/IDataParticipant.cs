using System.Collections.Generic;

namespace Model
{
    public interface IDataParticipant
    {
        public IParticipant Participant { get; set; }
        public string Track { get; set; }

        public void Add(List<IDataParticipant> list);

        public string GetBestParticipant(List<IDataParticipant> list);

        public List<IDataParticipant> GetParticipantsOrderedByBest(List<IDataParticipant> list, string track);
    }
}