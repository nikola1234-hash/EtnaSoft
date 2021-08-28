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
using EtnaSoft.Bll.Services;
using EtnaSoft.Bo.Entities;
using EtnaSoft.WPF.Events;
using Prism.Events;

namespace EtnaSoft.WPF.ViewModels
{
    public class CreateAppointmentViewModel : AppointmentWindowBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IResourceService _resourceService;
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
                RaisePropertyChanged(nameof(NumberOfPeople));
            }
        }

        private decimal _pricePerUnit;

        public decimal PricePerUnit
        {
            get { return _pricePerUnit; }
            set
            {
                _pricePerUnit = value;
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
                RaisePropertyChanged(nameof(SelectedStayType));
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
       
        #endregion

        public CreateAppointmentViewModel(AppointmentItem appointmentItem, SchedulerControl scheduler, IEventAggregator eventAggregator, IResourceService resourceService, DialogServiceViewModel dialogServiceViewModel, SearchGuestDialogViewModel searchGuestDialogViewModel) : base(appointmentItem, scheduler)
        {
            _eventAggregator = eventAggregator;
            _resourceService = resourceService;
         
            AppointmentItem = appointmentItem;
            SearchGuestDialogViewModel = searchGuestDialogViewModel;
            AddGuestDialogViewModel = dialogServiceViewModel;
            CreateReservationCommand = new DelegateCommand(CreateReservationExecute);
            SearchExistingGuestCommand = new DelegateCommand(SearchGuestDialogOpen);
            AbortReservationCreationCommand = new DelegateCommand(AbortExecute);
            LoadedCommand = new DelegateCommand(OnLoaded);
            AddNewGuestCommand = new DelegateCommand(AddNewGuestExecute);
            
           
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
                var id = (int) AppointmentItem.ResourceId;
                var rooms = _resourceService.CreateResource();
                List<Room> roomList = rooms.ToList();
                RoomList = rooms;
                SelectedRoom = rooms.FirstOrDefault(s => s.Id == id);
                SelectedIndex = roomList.IndexOf(SelectedRoom);

            }
            ComboboxLogic();
        }

        private void AbortExecute()
        {
            CloseWindow();
        }

        private void CreateReservationExecute()
        {
            // Create Reservation Logic
            // if reservation is succesfull
            // OnAppointmentCreation();

            OnAppointmentCreation();
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
