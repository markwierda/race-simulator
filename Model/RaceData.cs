using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class RaceData<T> where T : IDataParticipant
    {
        private readonly List<IDataParticipant> _list = new List<IDataParticipant>();

        public void Add(IDataParticipant value)
        {
            value.Add(_list);
        }

        public string GetBestParticipant()
        {
            if (!_list.Any())
            {
                return "";
            }

            return _list[0].GetBestParticipant(_list);
        }

        public List<IDataParticipant> GetParticipantsOrderedByBest()
        {
            if (!_list.Any())
            {
                return null;
            }

            return _list[0].GetParticipantsOrderedByBest(_list);
        }
    }
}