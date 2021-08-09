using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using DevExpress.Mvvm;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Scheduling;
using EtnaSoft.Bll.Services;
using EtnaSoft.Bo.Entities;

namespace EtnaSoft.WPF.ViewModels
{
    public class AppointmentViewModel : AppointmentWindowViewModel
    {
        private readonly AppointmentItem _appointmentItem;
        
        private readonly IBookingService _bookingService;

        #region Commands
        public DelegateCommand LoadedCommand { get; }
        public ICommand<object> ChangedCommand { get; }
        #endregion

        #region Properties

        private string _roomNumber;

        public string RoomNumber
        {
            get { return _roomNumber; }
            set
            {
                _roomNumber = value;
                RaisePropertyChanged(nameof(RoomNumber));
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
                _numberOfPeople = value;
                RaisePropertyChanged(nameof(NumberOfPeople));
            }
        }
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

        private string _address;

        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                RaisePropertyChanged(nameof(Address));
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

        private string _emailAddress;

        public string EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                _emailAddress = value;
                RaisePropertyChanged(nameof(EmailAddress));
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
                _uniqueNumber = value;
                RaisePropertyChanged(nameof(UniqueNumber));
            }
        }

        

        #endregion
        public AppointmentViewModel(AppointmentItem appointmentItem, SchedulerControl scheduler, IBookingService bookingService) : base(appointmentItem, scheduler)
        {
            _appointmentItem = appointmentItem;
            _bookingService = bookingService;
            LoadedCommand = new DelegateCommand(OnLoad);
            RemoveAppointmentCommand = new DelegateCommand(ExecuteRemove);
            SaveAppointmentCommand = new DelegateCommand(ExecuteSaveCommand);
            SaveAndCloseAppointmentCommand = new DelegateCommand(ExecuteSaveAndCloseCommand);
            ChangedCommand = new DelegateCommand<object>(OnChangeCommandExecute);
        }

        private void OnChangeCommandExecute(object obj)
        {

            //Todo: GET VALUE OF COMBOBOX
            if (obj is BarEditItem item)
            {
                var resource = item.EditValue;
                var r = (List<ResourceItem>)resource[0];
                var room = (Room) resource.SourceObject;
                RoomNumber = room.RoomNumber;
            }

        }

        private void ExecuteSaveAndCloseCommand()
        {
            throw new NotImplementedException();
        }

        private void ExecuteSaveCommand()
        {
            throw new NotImplementedException();
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


            var id = (int) _appointmentItem.CustomFields["StayTypeId"];
            var stayTypes = _bookingService.LoadStayTypes();
            var stayType = _bookingService.LoadStayTypes().FirstOrDefault(s => s.Id == id);
            StayTypes = stayTypes;
            SelectedStayType = stayType;
        } 
        
        private void ExecuteRemove()
        {
            
            throw new NotImplementedException();
        }
    }
}


