using ErtnaSoft.Bo.Entities;

namespace EtnaSoft.WPF.ViewModels
{
    public abstract class BaseRoomEditViewModel : EtnaBaseViewModel
    {
        private Room _room;

        public Room Room
        {
            get { return _room; }
            set
            {
                _room = value;
                RaisePropertyChanged(nameof(Room));
                CanExecute();
            }
        }

        public BaseRoomEditViewModel(Room room = null)
        {
            Room = room;
        }

        public abstract bool CanExecute();
        public abstract void Execute();
        public abstract void Abort();
    }
}
