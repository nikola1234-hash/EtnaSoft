using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Xpf.Scheduling;
using DevExpress.XtraScheduler.Outlook.Interop;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Services;
using EtnaSoft.Bll.Services.Facade;
using EtnaSoft.Bll.Stores;
using EtnaSoft.Bo.Entities;
using EtnaSoft.WPF.Events;
using Prism.Events;

namespace EtnaSoft.WPF.ViewModels
{
    public class CreateAppointmentViewModel : AppointmentWindowBase
    {
        
        private readonly IEventAggregator _eventAggregator;
        private readonly IComboboxFacade _comboboxFacade;
        private readonly ICreateReservationService _createReservation;
        public ICommand CreateReservationCommand { get; }
        public ICommand AbortReservationCreationCommand { get; }
        public ICommand SearchExistingGuestCommand { get; }
        public ICommand AddNewGuestCommand { get; }
        public ICommand LoadedCommand { get; }

        protected DialogServiceViewModel AddGuestDialogViewModel { get; private set; }
        protected SearchGuestDialogViewModel SearchGuestDialogViewModel { get; private set; }
        protected IDialogService NewGuestDialogService => GetService<IDialogService>("newGuestService");
        protected IDialogService ChoseGuestDialogService => GetService<IDialogService>("choseGuestService");
        protected IMessageBoxService MessageService
        {
            get => GetService<IMessageBoxService>();
        }
        private ICurrentWindowService WindowService
        {
            get
            {
                return GetService<ICurrentWindowService>();
            }
        }

        #region Properties
        private int _guestId;
        private decimal _totalPrice;

        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;
                RaisePropertyChanged(nameof(TotalPrice));
            }
        }

        private int _numberOfPeople;

        public int NumberOfPeople
        {
            get { return _numberOfPeople; }
            set
            {
                _numberOfPeople = value;
                TotalPriceChanged();
                RaisePropertyChanged(nameof(NumberOfPeople));
            }
        }

        private int _numberOfKids;

        public int NumberOfKids
        {
            get { return _numberOfKids; }
            set
            {
                _numberOfKids = value;
                TotalPriceChanged();
                RaisePropertyChanged(nameof(NumberOfKids));
            }
        }

        

        public decimal PricePerUnit
        {
            get { return  SelectedStayType?.Price ?? 0;  }
            set
            {
                
                RaisePropertyChanged(nameof(PricePerUnit));
            }
        }
        private ObservableCollection<Room> _roomList;
        public ObservableCollection<Room> RoomList
        {
            get { return _roomList; }
            set
            {
                SetProperty(ref _roomList, value, () => RoomList);

            }
        }

        private ObservableCollection<StayType> _stayTypes;

        public ObservableCollection<StayType> StayTypes
        {
            get { return _stayTypes; }
            set
            {
                
                _stayTypes = value;
                
                RaisePropertyChanged(nameof(StayTypes));
            }
        }

    

        private StayType _selectedStayType;

        public StayType SelectedStayType
        {
            get { return _selectedStayType; }
            set
            {
                _selectedStayType = value;
                TotalPriceChanged();
                RaisePropertyChanged(nameof(SelectedStayType));
                RaisePropertiesChanged(nameof(PricePerUnit));
                RaisePropertiesChanged(nameof(PricePerKid));
            }
        }

     

        public int NumberOfDays
        {
            get => EndDate.Date.Subtract(StartDate.Date).Days;
            set
            {
                RaisePropertiesChanged(nameof(NumberOfDays));
                TotalPriceChanged();
            }

        }

        private Room _selectedRoom;

        public Room SelectedRoom
        {
            get { return _selectedRoom; }
            set
            {
                _selectedRoom = value;
                SetProperty(ref _selectedRoom, value, () => SelectedRoom);
            }
        }

        private int _selectedIndex;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                RaisePropertyChanged(nameof(SelectedIndex));
            }
        }
        private AppointmentItem AppointmentItem { get; }
        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (StartDate > EndDate)
                {
                    _startDate = DateTime.Now;
                }
                _startDate = value;
                RaisePropertyChanged(nameof(StartDate));
                RaisePropertiesChanged(nameof(NumberOfDays));
                if(SelectedStayType !=null)
                    TotalPriceChanged();
            }
        }

        private DateTime _endDate = DateTime.Now.Date.AddDays(2);

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (EndDate <= StartDate)
                {
                    _endDate = DateTime.Now.Date.AddDays(2);
                }

                _endDate = value;
                RaisePropertyChanged(nameof(EndDate));
                RaisePropertiesChanged(nameof(NumberOfDays));
                if(SelectedStayType !=null)
                    TotalPriceChanged();
                
            }
        }

        public decimal PricePerKid
        {
            get => SelectedStayType?.Price / 2 ?? 0;
            set => RaisePropertiesChanged(nameof(PricePerKid));

        }

        #endregion

        public CreateAppointmentViewModel(AppointmentItem appointmentItem, SchedulerControl scheduler, IEventAggregator eventAggregator, DialogServiceViewModel dialogServiceViewModel, SearchGuestDialogViewModel searchGuestDialogViewModel, IComboboxFacade comboboxFacade, ICreateReservationService createReservation) : base(appointmentItem, scheduler)
        {
            _eventAggregator = eventAggregator;

            AppointmentItem = appointmentItem;
            SearchGuestDialogViewModel = searchGuestDialogViewModel;
            _comboboxFacade = comboboxFacade;
            _createReservation = createReservation;
            AddGuestDialogViewModel = dialogServiceViewModel;
            CreateReservationCommand = new DelegateCommand(CreateReservationExecute);
            SearchExistingGuestCommand = new DelegateCommand(SearchGuestDialogOpen);
            AbortReservationCreationCommand = new DelegateCommand(AbortExecute);
            LoadedCommand = new DelegateCommand(OnLoaded);
            AddNewGuestCommand = new DelegateCommand(AddNewGuestExecute);
            
           
        }

        private void TotalPriceChanged()
        {
            if (NumberOfKids > 0)
            {
                TotalPrice = ((SelectedStayType.Price * NumberOfPeople) + (NumberOfKids * (SelectedStayType.Price / 2))) * NumberOfDays;
            }

            if (NumberOfKids == 0)
            {
                TotalPrice = SelectedStayType.Price * NumberOfPeople * NumberOfDays;
                
            }
            
        }

        private void SearchGuestDialogOpen()
        {

            UICommand result= ChoseGuestDialogService.ShowDialog(SearchGuestDialogViewModel.DialogCommands,
                "Postojeci gost", viewModel: SearchGuestDialogViewModel);
            if (result != null)
            {
                if (result.Id is int id )
                {
                    //returns SelectedGuest.ID as UICommand id;
                    _guestId = id;
                }

            }
            
        }

        private void AddNewGuestExecute()
        {
            using (AddGuestDialogViewModel)
            {
                UICommand result = NewGuestDialogService.ShowDialog(AddGuestDialogViewModel.DialogCommands, "Registracija",
                    viewModel: AddGuestDialogViewModel);
                if (result != null)
                {
                    MessageService.Show("Dialog clicked button " + result.Id);
                
                }
            }
           
        }

        /// <summary>
        /// This method is called when framework element is loaded
        /// </summary>
        private void OnLoaded()
        {
            // Combobox function
            void ComboboxLogic()
            {
                //Room combobox
                var id = (int) AppointmentItem.ResourceId;
                Room selectedRoom;
                int selectedIndex;
                RoomList = _comboboxFacade.FillRoomCombobox(id,out selectedRoom, out selectedIndex);
                SelectedRoom = selectedRoom;
                SelectedIndex = selectedIndex;
                //StayTypes combobox
                StayTypes = _comboboxFacade.FillStayTypeCombobox();

            }

            void DateTimeLogic()
            {
                StartDate = AppointmentItem.Start;
                if (AppointmentItem.End > DateTime.MinValue)
                {
                    EndDate = AppointmentItem.End;
                }
                
            }
            ComboboxLogic();
            DateTimeLogic();
        }

        private void AbortExecute()
        {
            CloseWindow();
        }

        private void CreateReservationExecute()
        {
            RoomReservation CreateRoomReservation()
            {
                var output =new RoomReservation
                {
                    GuestId = _guestId,
                    CreatedBy = UserStore.CurrentUser,
                    RoomId = SelectedRoom.Id,
                    StayTypeId = SelectedStayType.Id
                };
                return output;
            }

            Reservation CreateReservationObject()
            {  //TODO: ADD number of kids eventually to the equation
                var output = new Reservation
                {
                    StartDate = StartDate,
                    EndDate = EndDate,
                    NumberOfPeople = NumberOfPeople,
                    TotalPrice = TotalPrice,
                    CreatedBy = UserStore.CurrentUser

                };
                return output;
            }
            // Create Reservation Logic
            // if reservation is succesfull
            // OnAppointmentCreation();
            if (_guestId > 0)
            {
                var roomRes = CreateRoomReservation();
                var reservation = CreateReservationObject();
                _createReservation.CreateReservationInTransaction(roomRes, reservation);
                OnAppointmentCreation();
                CloseWindow();
            }
            else
            {
                MessageService.Show("Gost nije izabran");
            }
        }


        /// <summary>
        /// The Scheduler Timeline gets bookings refreshed by this event
        /// It should get called when CreateReservationCommand is called
        /// </summary>
        private void OnAppointmentCreation()
        {
            _eventAggregator.GetEvent<AppointmentViewEvent>().Publish();
        }

        private void CloseWindow()
        {
            WindowService?.Close();
        }

        public override void Dispose()
        {
            base.Dispose();
            AddGuestDialogViewModel?.Dispose();
            SearchGuestDialogViewModel?.Dispose();

        }
    }
}
