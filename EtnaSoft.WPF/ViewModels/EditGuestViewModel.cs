using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Xpo.Logger;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Services;
using EtnaSoft.WPF.ViewModels.Base;
using Microsoft.Extensions.Logging;

namespace EtnaSoft.WPF.ViewModels
{
    public sealed class EditGuestViewModel : EditGuestViewModelBase
    {
        public Guest Guest { get; private set; }
        private readonly IUpdateGuestService _updateGuestService;
        private readonly IGuestHistoryService _guestHistoryService;
        private readonly ILogger<EditGuestViewModel> _logger;

        public delegate void UserDataChanged(object sender);
        public event UserDataChanged OnUserDataChange;

        public ICurrentWindowService WindowService
        {
            get
            {
                return this.GetService<ICurrentWindowService>();
            }
        }

        public IMessageBoxService MessageBoxService
        {
            get
            {
                return this.GetService<IMessageBoxService>();
            }
        }
        public ICommand SaveCommand { get; }
        public ICommand LoadCommand { get; }
        public ICommand CloseCommand { get; set; }

        private bool _isEditable = false;

        public bool IsEditable
        {
            get { return _isEditable; }
            set
            {
                _isEditable = value;
                RaisePropertyChanged(nameof(IsEditable));
            }
        }

        private ObservableCollection<GuestBookingHistory> _guestBookingHistories;
        public ObservableCollection<GuestBookingHistory> GuestBookingHistories
        {
            get { return _guestBookingHistories; }
            set
            {
                _guestBookingHistories = value;
                RaisePropertyChanged(nameof(GuestBookingHistories));
            }
        }
        public EditGuestViewModel(Guest guest, IUpdateGuestService updateGuestService, IGuestHistoryService guestHistoryService, ILogger<EditGuestViewModel> logger)
        {
            Guest = guest;
            _updateGuestService = updateGuestService;
            _guestHistoryService = guestHistoryService;
            _logger = logger;
            SaveCommand = new DelegateCommand(SaveData);
            LoadCommand = new DelegateCommand(OnLoad);
            CloseCommand = new DelegateCommand(CloseWindow);

        }

        private void WindowClose()
        {
            WindowService?.Close();
        }

        private void CloseWindow()
        {
            WindowClose();
        }

        public void OnLoad()
        {
            FirstName = Guest.FirstName;
            LastName = Guest.LastName;
            Address = Guest.Address;
            Telephone = Guest.Telephone;
            EmailAddress = Guest.EmailAddress;
            UniqueNumber = Guest.UniqueNumber;
            IsActive = Guest.IsActive;
            BirthDate = Guest.BirthDate;

            try
            {
                _logger.LogInformation("Getting guest history from db.");
                var guesthistory = _guestHistoryService.GetGuestBookingHistory(GuestId);
                GuestBookingHistories = new ObservableCollection<GuestBookingHistory>(guesthistory);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in assigning guest history.", ex);
                throw ex;
                
            }


        }

        public int GuestId => Guest.Id;
        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (value == _firstName)
                    return;
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
                if (value == _lastName)
                    return;
                _lastName = value;
                RaisePropertyChanged(nameof(LastName));
            }
        }

        private string _telephone;

        public string Telephone
        {
            get { return Guest.Telephone; }
            set
            {
                if (value == _telephone)
                    return;
                _telephone = value;
                RaisePropertyChanged(nameof(Telephone));
            }
        }

        private DateTime? _birthDate;

        public DateTime? BirthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                RaisePropertyChanged(nameof(BirthDate));
            }
        }
        private string _address;

        public string Address
        {
            get { return Guest.Address; }
            set
            {
                if (value == _address)
                    return;
                _address = value;
                RaisePropertyChanged(nameof(Address));
            }
        }

        private string _emailAddress;

        public string EmailAddress
        {
            get { return Guest.EmailAddress; }
            set
            {
                if (value == _emailAddress)
                    return;
                _emailAddress = value;
                RaisePropertyChanged(nameof(EmailAddress));
            }
        }

        private string _uniqueNumber;

        public string UniqueNumber
        {
            get { return _uniqueNumber; }
            set
            {
                if (value == Guest.UniqueNumber)
                    return;
                _uniqueNumber = value;
                RaisePropertyChanged(nameof(UniqueNumber));
            }
        }

        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (value == _isActive)
                    return;
                _isActive = value;
                RaisePropertyChanged(nameof(IsActive));
            }
        }

        public override void SaveData()
        {
            Guest guestToUpdate = new Guest()
            {
                FirstName = FirstName,
                LastName = LastName,
                Address = Address,
                Telephone = Telephone,
                EmailAddress = EmailAddress,
                UniqueNumber = UniqueNumber,
                IsActive = IsActive,
                DateCreated = Guest.DateCreated,
                DateModified = Guest.DateModified,
                CreatedBy = Guest.CreatedBy,
                BirthDate = BirthDate
            };
            try
            {
                _logger.LogInformation("Attempting to Update GuestData");
                _updateGuestService.UpdateGuestData(GuestId,guestToUpdate);
                _logger.LogInformation("Successful");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while updating Guest.", ex);
                throw ex;
            }

            
            OnUserDataChange?.Invoke(this);

            MessageBoxService.Show("Uspesno izmenjen zapis");
        }

        public override void Dispose()
        {
            GuestBookingHistories = null;
            base.Dispose();
        }
    }
}
