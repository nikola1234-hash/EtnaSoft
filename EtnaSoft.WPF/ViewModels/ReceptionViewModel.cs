using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Utils.CommonDialogs.Internal;
using DevExpress.Xpf.Bars.Native;
using DevExpress.Xpf.Scheduling;
using DevExpress.XtraScheduler.Native;
using EtnaSoft.Bll.Models;
using EtnaSoft.Bll.Services;
using EtnaSoft.Bll.Services.Facade;
using EtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Services;
using EtnaSoft.WPF.Events;
using EtnaSoft.WPF.Models;
using EtnaSoft.WPF.Services.Reception;
using EtnaSoft.WPF.Views;
using Prism.Events;

namespace EtnaSoft.WPF.ViewModels
{
    public class ReceptionViewModel : EtnaBaseViewModel
    {

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
        private readonly ICreateReservationService _createReservation;
        private readonly IComboboxFacade _comboboxFacade;
        private readonly IAvailableRoomsService _availableRooms;
        private readonly DialogServiceViewModel _dialogServiceViewModel;
        private readonly SearchGuestDialogViewModel _searchGuestDialogViewModel;
        private readonly IUpdateReservationDateDragService _dragUpdate;

        public ICommand<object> EditBookingCommand { get; }
        public ICommand LoadedCommand { get; }
        public ICommand<object> BookingDrag { get; }
        public ICommand<object> BookingResize { get; }
        public ReceptionViewModel(IResourceService roomResource, ISchedulerService schedulerService, IBookingService bookingService, IEventAggregator eventAggregator, IDetailsManager detailsManager, SearchGuestDialogViewModel searchGuestDialogViewModel, DialogServiceViewModel dialogServiceViewModel, IComboboxFacade comboboxFacade, ICreateReservationService createReservation, IAvailableRoomsService availableRooms, IUpdateReservationDateDragService dragUpdate)
        {
            EditBookingCommand = new DelegateCommand<object>(OnBookingWindowOpen);
            LoadedCommand = new DelegateCommand(OnLoad);
            BookingDrag = new DelegateCommand<object>(BookingDragCommand);
            BookingResize = new DelegateCommand<object>(BookingResizeCommand);
            _roomResource = roomResource;
            _bookingService = bookingService;
            _eventAggregator = eventAggregator;
            _detailsManager = detailsManager;


            _searchGuestDialogViewModel = searchGuestDialogViewModel;
            _dialogServiceViewModel = dialogServiceViewModel;
            _comboboxFacade = comboboxFacade;
            _createReservation = createReservation;
            _availableRooms = availableRooms;
            _dragUpdate = dragUpdate;
            _schedulerService = schedulerService;
        }

        private void BookingResizeCommand(object obj)
        {
            if (obj is CommitAppointmentResizeEventArgs booking)
            {
                DateTime startDate = booking.ResizeAppointment.Start;
                DateTime endDate = booking.ResizeAppointment.End;
                int id = (int)booking.SourceAppointment.Id;
                var answer = MessageBox.Show($"Da li zelite da promenite datume rezervacije na check in: {startDate}, check out: {endDate}?", "Obavestenje",
                    MessageBoxButton.YesNo);
                if (answer == MessageBoxResult.Yes)
                {
                    bool success = _dragUpdate.DragUpdateDate(id, startDate, endDate);
                    if (success)
                    {
                        MessageBox.Show($"Uspesno promenjen datum rezervacije ID: {id}");

                    }
                }
                PopulateBookings();
            }
        }

        private void BookingDragCommand(object obj)
        {
            if (obj is DropAppointmentEventArgs eventArgs)
            {
               var i = MessageBox.Show("Da li zelite da izmenite datum rezervacije?", "Obavestenje", MessageBoxButton.YesNo);
               if (i == MessageBoxResult.Yes)
               {
                   //TODO: Raskaciti event naci resenje!
                   eventArgs.SourceAppointments[0].ResourceIdsChanged += ReceptionViewModel_ResourceIdsChanged;
                   bool success = false;
                   bool isDateDirty = false;
                   var startDate = eventArgs.DragAppointments[0].Start;
                   var endDate = eventArgs.DragAppointments[0].End;
                   var sourceStartDate = eventArgs.SourceAppointments[0].Start;
                   var sourceEndDate = eventArgs.SourceAppointments[0].End;
                   isDateDirty = sourceStartDate != startDate || sourceEndDate != endDate;
                   if (isDateDirty)
                   {
                       var id = (int)eventArgs.SourceAppointments[0].Id;
                       success = _dragUpdate.DragUpdateDate(id,startDate, endDate);
                       if (success)
                       {
                           MessageBox.Show("Uspesno promenjen datum rezervacije");
                       }
                   }
               }
               //eventArgs.SourceAppointments[0].ResourceIdsChanged -= ReceptionViewModel_ResourceIdsChanged;
            }
        }

        private void ReceptionViewModel_ResourceIdsChanged(object sender, EventArgs e)
        
        {
            if (sender is AppointmentItem bookingItem)
            {
                int roomId = (int) bookingItem.ResourceId;
                int id = (int) bookingItem.Id;
                bool success = _dragUpdate.DragUpdateRoom(id, roomId);
                if (success)
                {
                    MessageBox.Show("Uspesno promenjena soba");
                }
                PopulateBookings();
            }
        }

        private void AppointmentOnStateChanged()
        {
            void UnsubscribeToAppointmentEventAggregator(SubscriptionToken token)
            {
                _eventAggregator.GetEvent<AppointmentViewEvent>().Unsubscribe(token);
            }

            PopulateBookings();
            UnsubscribeToAppointmentEventAggregator(_subToken);
        }

    
        void PopulateBookings()
        {
            if (Bookings == null)
            {
                Bookings = _schedulerService.LoadResource();
            }
            else
            {
                Bookings?.Clear();
                Bookings = _schedulerService.LoadResource();
            }
        }

        void PopulateRooms()
        {
            if (Rooms == null)
                Rooms = _roomResource.CreateResource();

        }
        private void OnLoad()
        {
            //Initialize
            PopulateRooms();
            PopulateBookings();
            PopulateLabels();
           
        }

        private void PopulateLabels()
        {
            
            var labels = _schedulerService.LoadCustomLabels();
            Labels = CustomLabel.Create(labels);
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
                    DataContext = new CreateAppointmentViewModel(args.Appointment, sender, _eventAggregator,
                                                                _dialogServiceViewModel, _searchGuestDialogViewModel, _comboboxFacade, _createReservation, _availableRooms)
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
            ReleaseResources();
        }

        private void ReleaseResources()
        {
            _subToken = null;
            _dialogServiceViewModel.Dispose();
            _searchGuestDialogViewModel.Dispose();
            Bookings.Clear();
            
            Rooms.Clear();
            Labels.Clear();
            Bookings = null;
            Rooms = null;
            Labels = null;
        }
    }
}
