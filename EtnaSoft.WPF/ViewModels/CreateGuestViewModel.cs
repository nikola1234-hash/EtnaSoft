using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Services;
using EtnaSoft.WPF.Helpers;

namespace EtnaSoft.WPF.ViewModels
{
    public sealed class CreateGuestViewModel : EtnaBaseViewModel, IDataErrorInfo
    {
        private bool _isDirty;
        private bool _isButtonEnabled;
        private bool allowValidation = false;
        
        public bool IsButtonEnabled
        {
            get { return _isButtonEnabled; }
            set
            {
                _isButtonEnabled = value;
                RaisePropertyChanged(nameof(IsButtonEnabled));
            }
        }

        private readonly ICreateGuestService _createGuestService;
        private ICurrentWindowService CurrentWindow => GetService<ICurrentWindowService>();
        public ICommand CreateGuestCommand { get; }

        public ICommand AbortCommand { get; }
        public delegate void DataChange();

        public event DataChange OnDataChange;
        public CreateGuestViewModel(ICreateGuestService createGuestService)
        {
            _createGuestService = createGuestService;
            CreateGuestCommand = new DelegateCommand(CreateGuest);
            AbortCommand = new DelegateCommand(CloseWindow);
        }
        public bool IsDirty
        {
            get => _isDirty;
            set
            {
                _isDirty = value;
                RaisePropertyChanged(nameof(IsDirty));
            }
        }

        private string _firstName;

        [Required]
        public string FirstName
        {
            get => _firstName;
            set => IsDirty = SetValue(ref _firstName, value);
        }

        private string _lastName;

        [Required]
        public string LastName
        {
            get => _lastName;
            set => IsDirty = SetValue(ref _lastName, value);
        }

        private string _telephone;

        [Required]
        public string Telephone
        {
            get => _telephone;
            set => IsDirty = SetValue(ref _telephone, value);
        }

        private string _emailAddress;

        public string EmailAddress
        {
            get => _emailAddress;
            set => IsDirty = SetValue(ref _emailAddress, value);
        }

        private string _address;

        public string Address
        {
            get => _address;
            set => IsDirty = SetValue(ref _address, value);
        }

        private string _uniqueNumber;

        public string UniqueNumber
        {
            get => _uniqueNumber;
            set => IsDirty = SetValue(ref _uniqueNumber, value);
        }

        private DateTime? _birthDate;

        public DateTime? BirthDate
        {
            get => _birthDate;
            set => SetValue(ref _birthDate, value);
        }

        private string EnableValidationAndGetError()
        {
            string error = ((IDataErrorInfo)this).Error;
            if (!string.IsNullOrEmpty(error))
            {
                this.RaisePropertyChanged();
                return error;
            }

            return null;
        }

        private void ClearFields()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Address = string.Empty;
            Telephone = string.Empty;
            EmailAddress = string.Empty;
            BirthDate = DateTime.Now.Date.AddYears(-20);
            UniqueNumber = string.Empty;

        }

        private void CloseWindow()
        {
            CurrentWindow?.Close();
        }
        private void CreateGuest()
        {
            string error = EnableValidationAndGetError();
            if (error != null) return;
            try
            {
                Guest newGuest = new Guest()
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Address = Address,
                    EmailAddress = EmailAddress,
                    BirthDate = BirthDate,
                    UniqueNumber = UniqueNumber,
                    Telephone = Telephone
                };
                var guest = _createGuestService.CreateGuest(newGuest);
                ClearFields();
                OnDataGridItemsChange();
                MessageBox.Show("Uspesno upisan gost.", "Obavestenje", MessageBoxButton.OK);
                CloseWindow();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        void OnDataGridItemsChange()
        {
            OnDataChange?.Invoke();
        }

        public string this[string columnName]
        {
            get
            {
                string firstName = BindableBase.GetPropertyName(() => FirstName);
                string lastName = BindableBase.GetPropertyName(() => LastName);
                string telephone = BindableBase.GetPropertyName(() => Telephone);
                if (columnName == firstName)
                {
                    return RequiredValidationRule.GetErrorMessage(firstName, FirstName);
                }
                else if (columnName == lastName)
                {
                    return RequiredValidationRule.GetErrorMessage(lastName, LastName);
                }
                else if (columnName == telephone)
                {
                    return RequiredValidationRule.GetErrorMessage(telephone, Telephone);
                }

                return null;
            }
        }

        public string Error
        {
            get
            {
                IDataErrorInfo me = (IDataErrorInfo)this;
                string error = me[BindableBase.GetPropertyName(() => FirstName)] +
                               me[BindableBase.GetPropertyName(() => LastName)] +
                               me[BindableBase.GetPropertyName(() => Telephone)];

                if (!String.IsNullOrEmpty(error))
                {
                    return "Molimo vas proverite unete podatke";
                }

                return null;

            }
        }

        public override void Dispose()
        {
            ClearFields();
            base.Dispose();
        }
    }
    
}
