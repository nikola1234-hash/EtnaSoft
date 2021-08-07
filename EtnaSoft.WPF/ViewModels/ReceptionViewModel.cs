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
        public ICommand<object> EditBookingCommand { get; }
        public ReceptionViewModel(IResourceService roomResource, IBookingService bookingService)
        {
            EditBookingCommand = new DelegateCommand<object>(ExecuteEditBooking);
            _roomResource = roomResource;
            _bookingService = bookingService;
            Initialize();
        }

        private void ExecuteEditBooking(object obj)
        {
            var args = (AppointmentWindowShowingEventArgs) obj;
            //Sender is source
            var sender = (SchedulerControl) args.Source;
            args.Window.DataContext = new AppointmentViewModel(args.Appointment, sender);

        }


        public void Initialize()
        {
            Rooms = _roomResource.CreateResource();
            Bookings = _bookingService.LoadResource();
        }
    }
}
