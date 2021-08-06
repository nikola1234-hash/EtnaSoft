using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using DevExpress.Xpf.Bars.Native;
using DevExpress.Xpf.Scheduling;
using EtnaSoft.Bll.Models;
using EtnaSoft.Bll.Services;
using EtnaSoft.Bo.Entities;

namespace EtnaSoft.WPF.ViewModels
{
    public class ReceptionViewModel : ViewModelBase
    {
        public ObservableCollection<Room> Rooms { get; set; }
        public ObservableCollection<Booking> Bookings { get; set; }
        private readonly IResourceService _roomResource;
        private readonly IBookingService _bookingService;
        public ReceptionViewModel(IResourceService roomResource, IBookingService bookingService)
        {
            _roomResource = roomResource;
            _bookingService = bookingService;
            Initialize();
        }

        public void Initialize()
        {
            Rooms = _roomResource.CreateResource();
            Bookings = _bookingService.LoadResource();
        }
    }
}
