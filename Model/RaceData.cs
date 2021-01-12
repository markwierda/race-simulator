using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Model
{
    public class RaceData<T> where T : IDataParticipant
    {
        private readonly List<IDataParticipant> _list = new List<IDataParticipant>();

        public event PropertyChangedEventHandler PropertyChanged;

        public void Add(IDataParticipant value)
        {
            value.Add(_list);
            OnPropertyChanged();
        }

        public string GetBestParticipant()
        {
            if (!_list.Any())
            {
                return "";
            }

            return _list[0].GetBestParticipant(_list);
        }

        public List<IDataParticipant> GetParticipantsOrderedByBest(string track)
        {
            if (!_list.Any())
            {
                return null;
            }

            return _list[0].GetParticipantsOrderedByBest(_list, track);
        }

        public void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}