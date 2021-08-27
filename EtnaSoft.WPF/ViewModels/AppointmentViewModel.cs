using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Scheduling;
using EtnaSoft.Bll.Services;
using EtnaSoft.Bo.Entities;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors;
using EtnaSoft.WPF.Events;
using EtnaSoft.WPF.Services.Reception;
using Prism.Events;

namespace EtnaSoft.WPF.ViewModels
{
    public class AppointmentViewModel : AppointmentWindowBase
    {
        private readonly AppointmentItem _appointmentItem;
        
        private readonly ISchedulerService _schedulerService;
        private readonly IBookingService _bookingService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDetailsManager _detailsManager;
        #region Commands
        public DelegateCommand LoadedCommand { get; }
        public ICommand SaveAndCloseCommand { get; }
        public ICommand<object> ChangedCommand { get; }
        public ICommand CheckInCommand { get; }
        #endregion

        #region Properties

        private string _roomNumber;

        public string RoomNumber
        {
            get { return _roomNumber; }
            set
            {
                if (string.IsNullOrWhiteSpace(_roomNumber))
                {
                    SetValue(ref _roomNumber, value);
                }
                else
                {
                    IsRoomReservationDirty = SetValue(ref _roomNumber, value);
                    RaisePropertiesChanged(nameof(SaveButtonEnabled));
                }
            }
        }

        private bool _isGuestDirty;

        public bool IsGuestDirty
        {
            get { return _isGuestDirty; }
            set
            {
                _isGuestDirty = value;
                RaisePropertyChanged(nameof(IsGuestDirty));
            }
        }

        private bool _isReservationDirty;

        public bool IsReservationDirty
        {
            get { return _isReservationDirty; }
            set
            {
                _isReservationDirty = value;
                RaisePropertyChanged(nameof(IsReservationDirty));
            }
        }

        private bool _isRoomReservationDirty;

        public bool IsRoomReservationDirty
        {
            get { return _isRoomReservationDirty; }
            set
            {
                _isRoomReservationDirty = value;
                RaisePropertyChanged(nameof(IsRoomReservationDirty));
            }
        }
        private int _numberOfPeople;
        private int _numberOfDays;
        public int NumberOfDays
        {
            get { return _appointmentItem.End.Subtract(_appointmentItem.Start).Days; }
            set
            {
                _numberOfDays = value;
                RaisePropertyChanged(nameof(NumberOfDays));
            }
        }
        public int NumberOfPeople
        {
            get { return _numberOfPeople; }
            set
            {
                if (_numberOfPeople != 0)
                {
                    IsReservationDirty = SetValue(ref _numberOfPeople, value);
                    RaisePropertiesChanged(nameof(SaveButtonEnabled));
                }
                else
                {
                    SetValue(ref _numberOfPeople, value);
                }
            }
        }
        private decimal _totalPrice;
        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                if (_totalPrice != 0)
                {
                    IsReservationDirty = SetValue(ref _totalPrice, value);
                    RaisePropertiesChanged(nameof(SaveButtonEnabled));
                }
                else
                {
                    SetValue(ref _totalPrice, value);
                }
            }
        }
        private StayType _selectedStayType;
        public StayType SelectedStayType
        {
            get { return _selectedStayType; }
            set
            {
                if (_selectedStayType is null)
                {
                    SetValue(ref _selectedStayType, value);
                }
                else
                {
                    IsRoomReservationDirty = SetValue(ref _selectedStayType, value);
                    RaisePropertiesChanged(nameof(SaveButtonEnabled));
                }
            }
        }

        public bool SaveButtonEnabled
        {
            get => IsRoomReservationDirty || IsGuestDirty || IsReservationDirty;
        }

        private List<StayType> _stayTypes;
        public List<StayType> StayTypes
        {
            get { return _stayTypes; }
            set
            {
                _stayTypes = value;
                RaisePropertyChanged(nameof(StayTypes));
            }
        }

        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (string.IsNullOrWhiteSpace(_firstName))
                {
                    SetValue(ref _firstName, value);
                }
                else
                {
                   IsGuestDirty = SetValue(ref _firstName, value);
                   RaisePropertiesChanged(nameof(SaveButtonEnabled));
                }
            }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (string.IsNullOrWhiteSpace(_lastName))
                {
                    SetValue(ref _lastName, value);
                }
                else
                {
                    IsGuestDirty = SetValue(ref _lastName, value);
                    RaisePropertiesChanged(nameof(SaveButtonEnabled));
                }
            }
        }

        private string _address;

        public string Address
        {
            get { return _address; }
            set
            {
                if (string.IsNullOrWhiteSpace(_address))
                {
                    SetValue(ref _address, value);
                }
                else
                {
                    IsGuestDirty = SetValue(ref _address, value);
                    RaisePropertiesChanged(nameof(SaveButtonEnabled));
                }
            }
        }

        private string _telephone;

        public string Telephone
        {
            get { return _telephone; }
            set
            {
                if (string.IsNullOrWhiteSpace(_telephone))
                {
                    SetValue(ref _telephone, value);
                }
                else
                {
                    IsGuestDirty = SetValue(ref _telephone, value);
                    RaisePropertiesChanged(nameof(SaveButtonEnabled));
                }
            }
        }

        private string _emailAddress;

        public string EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                if (string.IsNullOrWhiteSpace(_emailAddress))
                {
                    SetValue(ref _emailAddress, value);
                }
                else
                {
                    IsGuestDirty = SetValue(ref _emailAddress, value);
                    RaisePropertiesChanged(nameof(SaveButtonEnabled));
                }
            }
        }

        private bool _isCheckedIn;

        public bool IsCheckedIn
        {
            get { return _isCheckedIn; }
            set
            {
                _isCheckedIn = value;
                RaisePropertyChanged(nameof(IsCheckedIn));
            }
        }

        private bool _isCanceled;

        public bool IsCanceled
        {
            get { return _isCanceled; }
            set
            {
                _isCanceled = value;
                RaisePropertyChanged(nameof(IsCanceled));
            }
        }

        private string _uniqueNumber;

        public string UniqueNumber
        {
            get { return _uniqueNumber; }
            set
            {
                if (string.IsNullOrWhiteSpace(_uniqueNumber))
                {
                    SetValue(ref _uniqueNumber, value);
                }
                else
                {
                    IsGuestDirty = SetValue(ref _uniqueNumber, value);
                    RaisePropertiesChanged(nameof(SaveButtonEnabled));
                }
            }
        }

        private bool _checkInEnabled;

        public bool CheckInEnabled => _appointmentItem.Start.Date == DateTime.Now.Date;
        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != DateTime.MinValue)
                {
                    IsReservationDirty = SetValue(ref _startDate, value);
                    RaisePropertiesChanged(nameof(SaveButtonEnabled));
                }
                else
                {
                    SetValue(ref _startDate, value);
                }
            }
        }

        private DateTime _endDate;

        public DateTime EndDate

        {
            get { return _endDate; }
            set
            {
                if (_endDate != DateTime.MinValue)
                {
                    IsReservationDirty = SetValue(ref _endDate, value);
                    RaisePropertiesChanged(nameof(SaveButtonEnabled));
                }
                else
                {
                    SetValue(ref _endDate, value);
                }
            }
        }

        private DateTime _birthDate;

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                if (_birthDate != DateTime.MinValue)
                {
                    IsGuestDirty = SetValue(ref _birthDate, value);
                    RaisePropertiesChanged(nameof(SaveButtonEnabled));
                }
                else
                {
                    SetValue(ref _birthDate, value);
                }
            }
        }
        public ICurrentWindowService WindowService
        {
            get => GetService<ICurrentWindowService>(); 
        }
        #endregion
        public AppointmentViewModel(AppointmentItem appointmentItem, SchedulerControl scheduler, ISchedulerService schedulerService, IBookingService bookingService, IEventAggregator eventAggregator, IDetailsManager detailsManager) : base(appointmentItem, scheduler)
        {
            _appointmentItem = appointmentItem;
            _schedulerService = schedulerService;
            _bookingService = bookingService;
            _eventAggregator = eventAggregator;
            _detailsManager = detailsManager;


            CheckInCommand = new DelegateCommand(CheckInExecute);
            LoadedCommand = new DelegateCommand(OnLoad);
            RemoveAppointmentCommand = new DelegateCommand(ExecuteRemove);
            SaveAppointmentCommand = new DelegateCommand(ExecuteSaveCommand);
            SaveAndCloseCommand = new DelegateCommand(ExecuteSaveAndCloseCommand);
            ChangedCommand = new DelegateCommand<object>(OnChangeCommandExecute);
        }

        private void CheckInExecute()
        {
            int id;
            bool success = int.TryParse(_appointmentItem.Id.ToString(), out id);
            if (success)
            {
                IsCheckedIn = _bookingService.CheckIn(id);
                MessageBox.Show("Uspesno prijavljen gost");
                OnChangeReloadAppointments();
                CloseWindow();
            }
            else
            {
                throw new Exception("Error parsing Id");
            }
           
        }
        
        private void OnChangeCommandExecute(object obj)
        {
            //EditValue property need first to be casted as list of objects
            // Otherwise you get SystemCollections Generic `1 System.Object Error
            if (obj is BarEditItem item)
            {
                var resource = item.EditValue;
                if (resource is List<object> listResource)
                {
                    var resourceItem = (ResourceItem)listResource[0];
                    var room = (Room) resourceItem.SourceObject;
                    RoomNumber = room.RoomNumber;
                }
            }
        }

        private void ExecuteSaveAndCloseCommand()
        {
            var success = _detailsManager.CreateUpdateModel(this);
            if (success)
            {
                OnChangeReloadAppointments();
                CloseWindow();
            }
            else
            {
                throw new Exception("DetailsManager Update service exception!");
            }
        }

        private void ExecuteSaveCommand()
        {
           //throw new NotImplementedException();
        }

        //TODO: Map rest of custom fiels
        private void OnLoad()
        {

            //Textboxes
            NumberOfPeople = (int) _appointmentItem.CustomFields["NumberOfPeople"];
            TotalPrice = (decimal) _appointmentItem.CustomFields["TotalPrice"];
            FirstName = (string) _appointmentItem.CustomFields["FirstName"];
            LastName = (string) _appointmentItem.CustomFields["LastName"];
            Telephone = (string) _appointmentItem.CustomFields["Telephone"];
            Address = (string) _appointmentItem.CustomFields["Address"];
            EmailAddress = (string) _appointmentItem.CustomFields["EmailAddress"];
            IsCheckedIn = (bool)_appointmentItem.CustomFields["IsCheckedIn"];
            IsCanceled = (bool)_appointmentItem.CustomFields["IsCanceled"];
            UniqueNumber = (string) _appointmentItem.CustomFields["UniqueNumber"];
            RoomNumber = (string) _appointmentItem.CustomFields["RoomNumber"];
            StartDate = _appointmentItem.Start;
            EndDate = _appointmentItem.End;


            
            var id = (int) _appointmentItem.CustomFields["StayTypeId"];





            var stayTypes = _schedulerService.LoadStayTypes();
            var stayType = _schedulerService.LoadStayTypes().FirstOrDefault(s => s.Id == id);
            StayTypes = stayTypes;
            SelectedStayType = stayType;
        } 
        
        private void ExecuteRemove()
        {
            int id;
            var success = int.TryParse(_appointmentItem.Id.ToString(), out id);
            if (success)
            {
               IsCanceled = _bookingService.Cancel(id);
               OnChangeReloadAppointments();
               CloseWindow();
            }
            else
            {
                throw new Exception("Id parse error");
            }
        }

        private void CloseWindow()
        {
            WindowService?.Close();
        }

        protected virtual void OnChangeReloadAppointments()
        {
            _eventAggregator.GetEvent<AppointmentViewEvent>().Publish();
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}


