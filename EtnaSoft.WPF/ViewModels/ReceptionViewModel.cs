﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Xpf.Bars.Native;
using DevExpress.Xpf.Scheduling;
using DevExpress.XtraScheduler.Native;
using EtnaSoft.Bll.Models;
using EtnaSoft.Bll.Services;
using EtnaSoft.Bll.Services.Facade;
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
        private readonly DialogServiceViewModel _dialogServiceViewModel;
        private readonly SearchGuestDialogViewModel _searchGuestDialogViewModel;
        

        public ICommand<object> EditBookingCommand { get; }
        public ICommand LoadedCommand { get; }
        public ReceptionViewModel(IResourceService roomResource, ISchedulerService schedulerService, IBookingService bookingService, IEventAggregator eventAggregator, IDetailsManager detailsManager, SearchGuestDialogViewModel searchGuestDialogViewModel, DialogServiceViewModel dialogServiceViewModel, IComboboxFacade comboboxFacade, ICreateReservationService createReservation)
        {
            EditBookingCommand = new DelegateCommand<object>(OnBookingWindowOpen);
            LoadedCommand = new DelegateCommand(OnLoad);
            _roomResource = roomResource;
            _bookingService = bookingService;
            _eventAggregator = eventAggregator;
            _detailsManager = detailsManager;


            _searchGuestDialogViewModel = searchGuestDialogViewModel;
            _dialogServiceViewModel = dialogServiceViewModel;
            _comboboxFacade = comboboxFacade;
            _createReservation = createReservation;
            _schedulerService = schedulerService;
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
            if (_isFirstRun)
            {
                Bookings = _schedulerService.LoadResource();
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

        void PopulateRooms()
        {
            if (_isFirstRun)
            {
                Rooms = _roomResource.CreateResource();
            }
        }
        private void OnLoad()
        {
        
            //Initialize
            PopulateRooms();
            PopulateBookings();
            PopulateLabels();
            _isFirstRun = false;
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
                                                                _dialogServiceViewModel, _searchGuestDialogViewModel, _comboboxFacade, _createReservation)
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
