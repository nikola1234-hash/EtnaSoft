using System;
using System.Collections.Generic;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.Utils.CommonDialogs.Internal;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Services;
using EtnaSoft.WPF.ViewModels.Base;

namespace EtnaSoft.WPF.ViewModels
{
    public sealed class EditGuestViewModel : EditGuestViewModelBase
    {
        public Guest Guest { get; private set; }
        private readonly IUpdateGuestService _updateGuestService;
        public delegate void UserDataChanged(object sender);

        public IWindowService WindowService
        {
            get
            {
                return this.GetService<IWindowService>();
            }
        }

        public IMessageBoxService MessageBoxService
        {
            get
            {
                return this.GetService<IMessageBoxService>();
            }
        }

        public event UserDataChanged OnUserDataChange;
        
        public ICommand SaveCommand { get; }
        public ICommand LoadCommand { get; }


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
        public EditGuestViewModel(Guest guest, IUpdateGuestService updateGuestService)
        {
            Guest = guest;
            _updateGuestService = updateGuestService;
            SaveCommand = new DelegateCommand(SaveData);
            LoadCommand = new DelegateCommand(OnLoad);
           
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
                _updateGuestService.UpdateGuestData(GuestId,guestToUpdate);
                if (IsActive == false)
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
            OnUserDataChange?.Invoke(this);

            MessageBoxService.Show("Uspesno izmenjen zapis");
            this.WindowService.ReturnSuccess();
            this.WindowService.Close();

        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
