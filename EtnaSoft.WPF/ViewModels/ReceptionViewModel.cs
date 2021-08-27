using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Xpf.Bars.Native;
using DevExpress.Xpf.Scheduling;
using EtnaSoft.Bll.Models;
using EtnaSoft.Bll.Services;
using EtnaSoft.Bo.Entities;
using EtnaSoft.WPF.Events;
using EtnaSoft.WPF.Models;
using EtnaSoft.WPF.Services.Reception;
using EtnaSoft.WPF.Views;
using Prism.Events;

namespace EtnaSoft.WPF.ViewModels
{
    public class ReceptionViewModel : EtnaBaseViewModel
    {
        private readonly IGuestSearchService _guestSearch;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDetailsManager _detailsManager;
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

        private ObservableCollection<CustomLabel> _labels;

        public ObservableCollection<CustomLabel> Labels
        {
            get { return _labels; }
            set
            {
                _labels = value;
                RaisePropertyChanged(nameof(Labels));
            }
        }
        
        private bool _isFirstRun = true;
        private SubscriptionToken _subToken;
        private readonly IResourceService _roomResource;
        private readonly ISchedulerService _schedulerService;
        private readonly IBookingService _bookingService;
        private CreateAppointmentWindow NewBookingWindow
        {
            get { return GetService<CreateAppointmentWindow>(); }
        }
        public ICommand<object> EditBookingCommand { get; }
        public ICommand LoadedCommand { get; }
        public ReceptionViewModel(IResourceService roomResource, ISchedulerService schedulerService, IBookingService bookingService, IEventAggregator eventAggregator, IDetailsManager detailsManager, IGuestSearchService guestSearch)
        {
            EditBookingCommand = new DelegateCommand<object>(OnBookingWindowOpen);
            LoadedCommand = new DelegateCommand(OnLoad);
            _roomResource = roomResource;
            _bookingService = bookingService;
            _eventAggregator = eventAggregator;
            _detailsManager = detailsManager;
            _guestSearch = guestSearch;
            _schedulerService = schedulerService;
        }

      

        private void AppointmentOnStateChanged()
        {
            void UnsubscribeToAppointmentEventAggregator(SubscriptionToken token)
            {
                _eventAggregator.GetEvent<AppointmentViewEvent>().Unsubscribe(token);
            }

            PopulateItems();
            UnsubscribeToAppointmentEventAggregator(_subToken);
        }

    
        void PopulateItems()
        {
            if (_isFirstRun)
            {
                Rooms = _roomResource.CreateResource();
                Bookings = _schedulerService.LoadResource();
                _isFirstRun = false;
            }
            else
            {
                if(Bookings.Any())
                {
                    Bookings.Clear();
                }
                Bookings = _schedulerService.LoadResource();
            }

        }
        private void OnLoad()
        {
        
            //Initialize
            PopulateItems();
            

            //TODO: LABEL IMPLEMENTATION HERE
            //If decide to implement labels;
            //This is a good place to start
            //Load Custom table Will be set to obsollete intentionally
            //var labels = _schedulerService.LoadCustomLabels();
            //Labels = CustomLabel.Create(labels);
        }

        private void OnBookingWindowOpen(object appointmentEvent)
        {
            void SubscribeToAppointmentEventAggregator()
            {
                _subToken = _eventAggregator.GetEvent<AppointmentViewEvent>()
                    .Subscribe(AppointmentOnStateChanged);
            }

            var args = (AppointmentWindowShowingEventArgs) appointmentEvent;
            // Sender is source
            var sender = (SchedulerControl) args.Source;

            // Checking if appointment is empty
            // if(args.Appointment is null) expression is always false
            // This is how i solved it at the minute.
            if ((int) args.Appointment.Id == 0)
            {
                args.Window = new CreateAppointmentWindow
                {
                    DataContext = new CreateAppointmentViewModel(args.Appointment, sender, _eventAggregator, _roomResource, _guestSearch)
                };
            }
            else
            {
                args.Window.DataContext = new AppointmentViewModel(args.Appointment, sender, _schedulerService,
                    _bookingService, _eventAggregator, _detailsManager);
            }

            //Needs to be at the end of method
            SubscribeToAppointmentEventAggregator();

        }

        public override void Dispose()
        {

            base.Dispose();
        }
    }
}
