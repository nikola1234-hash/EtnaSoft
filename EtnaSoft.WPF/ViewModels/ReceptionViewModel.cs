using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using DevExpress.Xpf.Scheduling;
using EtnaSoft.Bll.Services;
using EtnaSoft.Bo.Entities;

namespace EtnaSoft.WPF.ViewModels
{
    public class ReceptionViewModel : ViewModelBase
    {
        public ObservableCollection<Room> Rooms { get; set; }
        private readonly IRoomResourceService _roomResource;
        public ReceptionViewModel(IRoomResourceService roomResource)
        {
            _roomResource = roomResource;
            Initialize();
        }

        public void Initialize()
        {
            Rooms = _roomResource.CreateResource();
        }
    }
}
