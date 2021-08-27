using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Xpf.Scheduling;
using EtnaSoft.Bll.Services;
using EtnaSoft.Bo.Entities;
using EtnaSoft.WPF.Events;
using Prism.Events;

namespace EtnaSoft.WPF.ViewModels
{
    public class CreateAppointmentViewModel : AppointmentWindowViewModel, IDisposable
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IResourceService _resourceService;
        private readonly IGuestSearchService _guestSearch;
        public ICommand CreateReservationCommand { get; }
        public ICommand AbortReservationCreationCommand { get; }
        public ICommand SearchExistingGuestCommand { get; }
        public ICommand AddNewGuestCommand { get; }
        public ICommand LoadedCommand { get; }
        private int _guestId;
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
        private ObservableCollection<Room> _roomList;
        public ObservableCollection<Room> RoomList
        {
            get { return _roomList; }
            set
            {
                SetProperty(ref _roomList, value, () => RoomList);

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
        private AppointmentItem _appointmentItem { get; }
       
        #endregion

        public CreateAppointmentViewModel(AppointmentItem appointmentItem, SchedulerControl scheduler, IEventAggregator eventAggregator, IResourceService resourceService, IGuestSearchService guestSearch) : base(appointmentItem, scheduler)
        {
            _eventAggregator = eventAggregator;
            _resourceService = resourceService;
            _guestSearch = guestSearch;
            _appointmentItem = appointmentItem;

            CreateReservationCommand = new DelegateCommand(CreateReservationExecute);
            SearchExistingGuestCommand = new DelegateCommand(SearchGuestDialogOpen);
            AbortReservationCreationCommand = new DelegateCommand(AbortExecute);
            LoadedCommand = new DelegateCommand(OnLoaded);
            AddNewGuestCommand = new DelegateCommand(AddNewGuestExecute);
            AddGuestDialogViewModel = new DialogServiceViewModel();
           
        }

        private void SearchGuestDialogOpen()
        {
            using (SearchGuestDialogViewModel = new SearchGuestDialogViewModel(_guestSearch))
            {
                UICommand resultCommand = ChoseGuestDialogService.ShowDialog(SearchGuestDialogViewModel.DialogCommands,
                    "Postojeci gost", viewModel: SearchGuestDialogViewModel);
                if (resultCommand != null)
                {
                    _guestId = (int)resultCommand.Id;
                    MessageService.Show("Clicked" + resultCommand.Id);
                }
            }
        }

        private void AddNewGuestExecute()
        {
            UICommand result = NewGuestDialogService.ShowDialog(AddGuestDialogViewModel.DialogCommands, "Registracija",
                viewModel: AddGuestDialogViewModel);
            if (result != null)
            {
                MessageService.Show("Dialog clicked button " + result.Caption);
                
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
                var id = (int) _appointmentItem.ResourceId;
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

        public void Dispose()
        {
            AddGuestDialogViewModel?.Dispose();
            SearchGuestDialogViewModel?.Dispose();
        }
    }
}
