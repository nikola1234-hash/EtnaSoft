using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using DevExpress.CodeParser;
using DevExpress.Mvvm;
using DevExpress.Xpf.Scheduling;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.BulkSms.Services;
using EtnaSoft.Bll.Models;
using EtnaSoft.Bll.Services;
using EtnaSoft.Bll.Services.Facade;
using EtnaSoft.Dal.Services;
using EtnaSoft.Dal.Stores;
using EtnaSoft.WPF.Events;
using EtnaSoft.WPF.Services.SmsService;
using Prism.Events;

namespace EtnaSoft.WPF.ViewModels
{
    public enum TipPromocije
    {
        Vaucer
    }
    public class CreateAppointmentViewModel : AppointmentWindowBase
    {
        
        private readonly IEventAggregator _eventAggregator;
        private readonly IComboboxFacade _comboboxFacade;
        private readonly ICreateReservationService _createReservation;
        private readonly IAvailableRoomsService _availableRooms;
        private readonly ISpecialTypeService _specialTypeService;
        public ICommand CreateReservationCommand { get; }
        public ICommand AbortReservationCreationCommand { get; }
        public ICommand SearchExistingGuestCommand { get; }
        public ICommand AddNewGuestCommand { get; }
        public ICommand LoadedCommand { get; }
        public ICommand AvansCommand { get; }

        protected DialogServiceViewModel AddGuestDialogViewModel { get; private set; }
        protected SearchGuestDialogViewModel SearchGuestDialogViewModel { get; private set; }
        protected IDialogService NewGuestDialogService => GetService<IDialogService>("newGuestService");
        protected IDialogService ChoseGuestDialogService => GetService<IDialogService>("choseGuestService");
        private readonly IGuestSearchService _guestSearchService;
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

        private ObservableCollection<ProcenatModel> _procenatList = new ObservableCollection<ProcenatModel>()
        {
            new ProcenatModel()
            {
                Naziv = "10%",
                Procenat = 10
            },
            new ProcenatModel()
            {
                Naziv = "20%",
                Procenat = 20
            },
            new ProcenatModel()
            {
                Naziv = "30%",
                Procenat = 30
            }

        };

        public ObservableCollection<ProcenatModel> ProcenatList
        {
            get { return _procenatList; }
            set
            {
                _procenatList = value;
                RaisePropertyChanged(nameof(ProcenatList));
            }
        }

        private ProcenatModel _selectedAvans;

        public ProcenatModel SelectedAvans
        {
            get { return _selectedAvans; }
            set
            {
                _selectedAvans = value;
                CalculateAvans();
                
            }
        }

        private void RaiseAvansGroupChange()
        {
            RaisePropertyChanged(nameof(SelectedAvans));
            RaisePropertyChanged(nameof(Avans));
            RaisePropertyChanged(nameof(RemainingPrice));
            RaisePropertyChanged(nameof(SubTotal));
        }

        private bool _isAvans;

        public bool IsAvans
        {
            get { return _isAvans; }
            set
            {
                if (IsAvans)
                {
                    _subTotal = TotalPrice;
                    RaisePropertyChanged(nameof(SubTotal));
                    CalculateAvans();
                }
                _isAvans = value;
                RaisePropertyChanged(nameof(IsAvans));
            }
        }

        private void CalculateAvans()
        {
            if (!IsAvans)
                return;
            decimal percent = 0M;
            if (SelectedAvans != null)
            {
                switch(SelectedAvans.Procenat)
                {
                    case 20:
                        percent = 0.2M;
                        break;
                    case 10:
                        percent = 0.1M;
                        break;
                    case 30:
                        percent = 0.3M;
                        break;
                    default:
                        percent = 0M;
                        break;
                }
            }

            SubTotal = _totalPrice;
            Avans = _totalPrice * percent;
            RemainingPrice = _totalPrice - _avans;
            RaiseAvansGroupChange();
        }


        private decimal _remainingPrice;

        public decimal RemainingPrice
        {
            get { return _remainingPrice; }
            set
            {
                _remainingPrice = value;
                RaisePropertyChanged(nameof(RemainingPrice));
            }
        }

        private decimal _avans;

        public decimal Avans
        {
            get { return _avans; }
            set
            {
                
                _avans = value;
                RaisePropertyChanged(nameof(Avans));
            }
        }


        private decimal _subTotal;

        public decimal SubTotal
        {
            get { return _subTotal; }
            set
            {
                _subTotal = value;
                RaisePropertyChanged(nameof(SubTotal));
            }
        }

        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                if (IsAvans)
                {
                    _subTotal = value;
                    CalculateAvans();
                }
                _totalPrice = value;
                RaisePropertyChanged(nameof(TotalPrice));
                RaisePropertyChanged(nameof(SubTotal));

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


        private string _numberOfPeopleLabel = "Broj osoba:";

        public string NumberOfPeopleLabel
        {
            get { return _numberOfPeopleLabel; }
            set
            {
                _numberOfPeopleLabel = value;
                RaisePropertyChanged(nameof(NumberOfPeopleLabel));
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
                if (SelectedStayType.IsSpecialType)
                {
                    _numberOfPeopleLabel = "Broj promocija:";
                    RaisePropertiesChanged(nameof(NumberOfPeopleLabel));
                }
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
                LoadAvailableRooms(StartDate, EndDate);
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
                LoadAvailableRooms(StartDate, EndDate);
                
            }
        }

        private string _firstName
;

        public string FirstName

        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                RaisePropertyChanged(nameof(FirstName));
            }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                RaisePropertyChanged(nameof(LastName));
            }
        }

        private string _telephone;

        public string Telephone
        {
            get { return _telephone; }
            set
            {
                _telephone = value;
                RaisePropertyChanged(nameof(Telephone));
            }
        }

        public decimal PricePerKid
        {
            get => SelectedStayType?.Price / 2 ?? 0;
            set => RaisePropertiesChanged(nameof(PricePerKid));

        }

        #endregion

        public CreateAppointmentViewModel(AppointmentItem appointmentItem, SchedulerControl scheduler, IEventAggregator eventAggregator, DialogServiceViewModel dialogServiceViewModel, SearchGuestDialogViewModel searchGuestDialogViewModel, IComboboxFacade comboboxFacade, ICreateReservationService createReservation, IAvailableRoomsService availableRooms, ISpecialTypeService specialTypeService, IGuestSearchService guestSearchService) : base(appointmentItem, scheduler)
        {
            _eventAggregator = eventAggregator;

            AppointmentItem = appointmentItem;
            SearchGuestDialogViewModel = searchGuestDialogViewModel;
            _comboboxFacade = comboboxFacade;
            _createReservation = createReservation;
            _availableRooms = availableRooms;
            _specialTypeService = specialTypeService;
            _guestSearchService = guestSearchService;
            AddGuestDialogViewModel = dialogServiceViewModel;
            CreateReservationCommand = new DelegateCommand(CreateReservationExecute);
            SearchExistingGuestCommand = new DelegateCommand(SearchGuestDialogOpen);
            AbortReservationCreationCommand = new DelegateCommand(AbortExecute);
            LoadedCommand = new DelegateCommand(OnLoaded);
            AddNewGuestCommand = new DelegateCommand(AddNewGuestExecute);
            AvansCommand = new DelegateCommand(EnableOrDisableAvans);

        }

        private void EnableOrDisableAvans()
        {
            _isAvans = !_isAvans;
            RaisePropertyChanged(nameof(IsAvans));
        }

        /// <summary>
        /// Loads Available Rooms for dates
        /// </summary>
        private void LoadAvailableRooms(DateTime startDate, DateTime endDate)
        {
            if (RoomList != null)
            {
                
                if (RoomList.Any())
                {
                    RoomList.Clear();
                }

                RoomList = _availableRooms.LoadAvailableRooms(startDate, endDate);
            }
        }
        private void TotalPriceChanged()
        {
            
           
            if (SelectedStayType != null && SelectedStayType.IsSpecialType)
            {
                    PricePerKid = _specialTypeService.GetPricePerChild(SelectedStayType.Id);
                    TotalPrice = SelectedStayType.Price + PricePerKid;
                    return;
            }
            
            
            if (NumberOfKids > 0)
            {
                if (SelectedStayType != null)
                {
                    TotalPrice = ((SelectedStayType.Price * NumberOfPeople) + (NumberOfKids * (SelectedStayType.Price / 2))) * NumberOfDays;
                }
          
            }

            if (NumberOfKids == 0)
            {
                if (SelectedStayType != null)
                {
                    TotalPrice = SelectedStayType.Price * NumberOfPeople * NumberOfDays; 
                }
                
            }
            CalculateAvans();
        }
        /// <summary>
        /// Clear the fields
        /// </summary>
        void ClearGuestFields()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Telephone = string.Empty;
        }
     
        /// <summary>
        /// Search Guest in database,
        /// Dialog result returns guest Id 
        /// </summary>
        private void SearchGuestDialogOpen()
        {
        

            UICommand result= ChoseGuestDialogService.ShowDialog(SearchGuestDialogViewModel.DialogCommands,
                "Postojeci gost", viewModel: SearchGuestDialogViewModel);
            if (result != null)
            {
                if (result.Id is Guest guest )
                {
                    _guestId = guest.Id;
                    ClearGuestFields();
                    FirstName = guest.FirstName;
                    LastName = guest.LastName;
                    Telephone = guest.Telephone;
                }

            }
            
        }
        /// <summary>
        /// Adds Guest to database, returns newly created guest Id
        /// </summary>
        private void AddNewGuestExecute()
        {
            using (AddGuestDialogViewModel)
            {
                UICommand result = NewGuestDialogService.ShowDialog(AddGuestDialogViewModel.DialogCommands, "Registracija",
                    viewModel: AddGuestDialogViewModel);
                if (result != null)
                {
                    if (result.Id is Guest guest)
                    {
                        _guestId = guest.Id;
                        ClearGuestFields();
                        FirstName = guest.FirstName;
                        LastName = guest.LastName;
                        Telephone = guest.Telephone;
                        
                    }
                  
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
        /// <summary>
        /// Close Application Windows
        /// </summary>
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
                    InvoiceId = 0,
                    CreatedBy = UserStore.CurrentUser

                };
                return output;
            }
            // Create Reservation Logic
            // if reservation is succesfull
            // OnAppointmentCreation();
            if (_guestId > 0)
            {
                var invoice = new Invoice()
                {
                    Avans = Avans,
                    SubTotal = TotalPrice,
                    TotalPrice = TotalPrice - Avans
                };
                var roomRes = CreateRoomReservation();
                var reservation = CreateReservationObject();
                var reservationCreated = _createReservation.CreateReservationInTransaction(roomRes, reservation, invoice);
                if (IsAvans && reservationCreated)
                {
                    SendSmsAsync(reservation, invoice);
                }
                else if (IsAvans && !reservationCreated)
                {
                    MessageService.Show("Neuspesno kreiranje rezervacije");
                }
                
                OnAppointmentCreation();
                CloseWindow();
            }
            else
            {
                MessageService.Show("Gost nije izabran");
            }
        }

        private async void SendSmsAsync(Reservation reservation, Invoice invoice)
        {
            ISmsFacade smsFacade = new SmsFacade();
            
            var url = smsFacade.GetValue(ConfigKeys.SmsUrl);
            var user = smsFacade.GetValue(ConfigKeys.SmsUser);
            var secret = smsFacade.GetValue(ConfigKeys.SmsSecret);

            AvansSmsService avansService = new AvansSmsService(url, user, secret);
            var guest = _guestSearchService.GetGuestById(_guestId);
            var message = await avansService.SendSmsTask(guest, reservation, invoice);
            MessageService.Show(message);
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
            AddGuestDialogViewModel?.Dispose();
            SearchGuestDialogViewModel?.Dispose();
            base.Dispose();
        }
    }
}
