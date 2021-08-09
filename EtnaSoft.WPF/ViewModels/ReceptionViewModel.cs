using System.Collections.ObjectModel;
using System.Windows.Input;
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
        private ObservableCollection<Room> _rooms;

        public ObservableCollection<Room> Rooms
        {
            get { return _rooms; }
            set
            {
                _rooms = value;
                RaisePropertyChanged(nameof(Rooms));
            }
        }

        private ObservableCollection<Booking> _bookings;

        public ObservableCollection<Booking> Bookings
        {
            get { return _bookings; }
            set
            {
                _bookings = value;
                RaisePropertyChanged(nameof(Bookings));
            }
        }


        private readonly IResourceService _roomResource;
        private readonly IBookingService _bookingService;
        public ICommand<object> EditBookingCommand { get; }
        public ICommand LoadedCommand { get; }
        public ReceptionViewModel(IResourceService roomResource, IBookingService bookingService)
        {
            EditBookingCommand = new DelegateCommand<object>(ExecuteEditBooking);
            LoadedCommand = new DelegateCommand(OnLoad);
            _roomResource = roomResource;
            _bookingService = bookingService;
        }

        private void OnLoad()
        {
            //Initialize
            Rooms = _roomResource.CreateResource();
            Bookings = _bookingService.LoadResource();
            
        }

        private void ExecuteEditBooking(object obj)
        {
            var args = (AppointmentWindowShowingEventArgs) obj;
            //Sender is source
            var sender = (SchedulerControl) args.Source;
            args.Window.DataContext = new AppointmentViewModel(args.Appointment, sender, _bookingService);
            }
      
      }
}
