using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DevExpress.DocumentServices.ServiceModel.ServiceOperations;
using DevExpress.Mvvm;
using DevExpress.Xpf.Scheduling;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Models;
using EtnaSoft.Bll.Services;
using EtnaSoft.Bll.Services.Facade;
using EtnaSoft.Dal.Services;
using EtnaSoft.WPF.Events;
using EtnaSoft.WPF.Services.Authentication;
using EtnaSoft.WPF.Services.Reception;
using EtnaSoft.WPF.Views;
using Prism.Events;
using CustomLabel = EtnaSoft.WPF.Models.CustomLabel;

namespace EtnaSoft.WPF.ViewModels
{
    public sealed class ReceptionViewModel : EtnaBaseViewModel
    {
        
        private readonly IEventAggregator _eventAggregator;
        private readonly IDetailsManager _detailsManager;
        private readonly ISpecialTypeService _specialTypeService;
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

        private ObservableCollection<Booking> _bookings = new ObservableCollection<Booking>();

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
        private readonly IAuthenticator _authenticator;
        public ICommand<object> EditBookingCommand { get; }
        public ICommand LoadedCommand { get; }
        public ICommand<object> BookingDrag { get; }
        public ICommand<object> BookingResize { get; }
        public ICommand<object> CheckInCommand { get; }
        public ICommand<object> InvoicesCommand { get; }
        public ICommand<object> DeleteReservationCommand { get; }
        public ICommand<object> FetchAppointmentsCommand { get; }
        public ICommand<object>PopUpMenuShowingCommand { get; }

        private INotificationService NotificationService
        {
            get => this.GetService<INotificationService>();
        }

        public ReceptionViewModel(IResourceService roomResource, ISchedulerService schedulerService,
            IBookingService bookingService, IEventAggregator eventAggregator, IDetailsManager detailsManager,
            SearchGuestDialogViewModel searchGuestDialogViewModel, DialogServiceViewModel dialogServiceViewModel,
            IComboboxFacade comboboxFacade, ICreateReservationService createReservation,
            IAvailableRoomsService availableRooms, IUpdateReservationDateDragService dragUpdate, IAuthenticator authenticator, ISpecialTypeService specialTypeService)
        {
            EditBookingCommand = new DelegateCommand<object>(OnBookingWindowOpen);
            LoadedCommand = new DelegateCommand(OnLoad);
            BookingDrag = new DelegateCommand<object>(BookingDragCommand);
            BookingResize = new DelegateCommand<object>(BookingResizeCommand);
            CheckInCommand = new DelegateCommand<object>(ReservationCheckIn);
            InvoicesCommand = new DelegateCommand<object>(ShowInvoicePanel);
            DeleteReservationCommand = new DelegateCommand<object>(DeleteReservation);
            PopUpMenuShowingCommand = new DelegateCommand<object>(PopUpMenuShowing);
            FetchAppointmentsCommand = new DelegateCommand<object>(OnFetchAppointments);
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
            _authenticator = authenticator;
            _specialTypeService = specialTypeService;

            _schedulerService = schedulerService;
        }

        /// <summary>
        /// When Month View Changes Unload Old Bookings and load new ones
        /// </summary>
        private void OnFetchAppointments(object obj)
        {
            if (obj is VisibleIntervalsChangedEventArgs args)
            {
                var index = args.VisibleIntervals.Count();
                var startDate = args.VisibleIntervals.ElementAt(0);
                var endDate = args.VisibleIntervals.ElementAt(index - 1);
                //Just in case if its null
                if (Bookings == null)
                {
                    Bookings = _schedulerService.LoadResource(startDate.Start, endDate.Start);
                    return;
                }
                if (Bookings.Any())
                {
                    Bookings.Clear();
                }
                Bookings = _schedulerService.LoadResource(startDate.Start, endDate.Start);
            }
        
        }

        private void PopUpMenuShowing(object obj)
        {
           //TODO: Context menu PopUp Customization if needed
        }

        public void Logout()
        {
            _authenticator.Logout();
        }
        public static string ApplicationId
        {
            get { return "ReceptionView"; }
        }
        public void ShowNotification(string message)
        {
            INotification notification = NotificationService
                .CreatePredefinedNotification("Imate novu notifikaciju", 
                    message , DateTime.Now.Date.ToShortDateString());
            
            notification.ShowAsync();
        }
        private void DeleteReservation(object reservation)
        {
            if (reservation is AppointmentItem booking)
            {
                int id = (int) booking.Id;
                var result = MessageBox.Show("Da li ste sigurni da zelite da izbrisete izabranu rezervaciju?",
                    "Upozorenje", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _bookingService.Cancel(id);
                    PopulateBookings();
                }
            }
        }

        private void ShowInvoicePanel(object reservation)
        {
            throw new NotImplementedException();
        }

        private void ReservationCheckIn(object reservation)
        {
            if (reservation is AppointmentItem booking)
            {
                
                int id = (int)booking.Id;
                var isCheckedIn = _bookingService.CheckInStatus(id);
                if (!isCheckedIn)
                {
                    _bookingService.CheckIn(id);
                    PopulateBookings((DateTime)booking.Start, (DateTime)booking.Start.AddDays(7));
                }
                else
                {
                    MessageBox.Show("Rezervacija je vec prijavljena");
                }
            }
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

                // Populate from startDate + 7 days
                PopulateBookings(startDate, startDate.AddDays(7));
            }
        }

        private void BookingDragCommand(object obj)
        {
            if (obj is DropAppointmentEventArgs eventArgs)
            {
               var answeResult = MessageBox.Show("Da li zelite da izmenite datum rezervacije?", "Obavestenje", MessageBoxButton.YesNo);
               if (answeResult == MessageBoxResult.Yes)
               {
                   //TODO: Raskaciti event naci resenje!
                   eventArgs.SourceAppointments[0].ResourceIdsChanged += ReceptionViewModel_ResourceIdsChanged;
                   var startDate = eventArgs.DragAppointments[0].Start;
                   var endDate = eventArgs.DragAppointments[0].End;
                   var sourceStartDate = eventArgs.SourceAppointments[0].Start;
                   var sourceEndDate = eventArgs.SourceAppointments[0].End;
                   var isDateDirty = sourceStartDate != startDate || sourceEndDate != endDate;
                   if (isDateDirty)
                   {
                       var id = (int)eventArgs.SourceAppointments[0].Id;
                       var success = _dragUpdate.DragUpdateDate(id,startDate, endDate);
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

    
        void PopulateBookings(object startDate = null, object endDate = null)
        {
            DateTime sDate = DateTime.Now.Date;
            DateTime eDate = DateTime.Now.Date.AddDays(7);
            if (startDate != null && endDate != null)
            {
                sDate = (DateTime)startDate;
                eDate = (DateTime)endDate;
                startDate = null;
                endDate = null;
            }

            if (Bookings == null)
            {
                Bookings = _schedulerService.LoadResource(sDate, eDate);
            }
            else
            {
                Bookings?.Clear();
                Bookings = _schedulerService.LoadResource(sDate, eDate);
            }
        }

        void PopulateRooms()
        {
            if (Rooms == null)
                Rooms = _roomResource.CreateResource();

        }
        private void OnLoad()
        {
            string soba = "sobu";
            string ima = "imate";
            var number = _schedulerService.BookingsComingToday();
            if (number > 1 && number <= 4)
            {
                soba = $"{number} sobe";
            }
            else if (number > 5)
            {
                soba = $"{number} soba";
            }
            else if (number == 0)
            {
                soba = "soba";
                ima = "nemate";
            }
            ShowNotification($"Danas {ima} {soba} na dolasku.");
            //Initialize
            PopulateRooms();
            //PopulateBookings();
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
                                                                _dialogServiceViewModel, _searchGuestDialogViewModel, _comboboxFacade, _createReservation, _availableRooms, _specialTypeService)
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
            //Bookings.Clear();c# 
            
            //Rooms.Clear();
            //Labels.Clear();
            //Bookings = null;
            //Rooms = null;
            //Labels = null;
        }
    }
}
