using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.Mvvm;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Services;
using EtnaSoft.WPF.Helpers;
using EtnaSoft.WPF.Views;
using EtnaSoft.WPF.Window;

namespace EtnaSoft.WPF.ViewModels
{
    public sealed class CreateGuestContentViewModel : ContentViewModel, IDataErrorInfo, IDisposable
    {
        #region Fields

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

        private Guest _selectedGuest;

        public Guest SelectedGuest
        {
            get { return _selectedGuest; }
            set
            {
                _selectedGuest = value;
                RaisePropertyChanged(nameof(SelectedGuest));
            }
        }

        private ObservableCollection<Guest> _dataGrid = new ObservableCollection<Guest>();

        public ObservableCollection<Guest> DataGrid
        {
            get { return ReturnGuests(); }
            set
            {
                if (value == _dataGrid)
                {
                    return;
                }
                _dataGrid = value;
                RaisePropertyChanged(nameof(DataGrid));
            }
        }

        #endregion


        private readonly ICreateGuestService _createGuestService;
        private readonly IGuestSearchService _guestSearchService;
        private readonly IUpdateGuestService _updateGuestService;
        private readonly IGuestDataGridService _guestDataGridService;
        public ICommand CreateGuestCommand { get; }
        public ICommand LoadCommand { get; }
        public ICommand<object> CellDoubleClickCommand { get; }
        
        public CreateGuestContentViewModel(ICreateGuestService createGuestService, IGuestSearchService guestSearchService, IUpdateGuestService updateGuestService, IGuestDataGridService guestDataGridService)
        {
            _createGuestService = createGuestService;
            _guestSearchService = guestSearchService;
            _updateGuestService = updateGuestService;
            _guestDataGridService = guestDataGridService;
            CreateGuestCommand = new DelegateCommand(CreateGuest);
            LoadCommand = new DelegateCommand(OnLoad);
            CellDoubleClickCommand = new DelegateCommand<object>(OnCellDoubleClick);

        }

        private void OnCellDoubleClick(object obj)
        {
            var guest = (Guest)obj;
            var viewModel = new EditGuestViewModel(guest, _updateGuestService);
            var window = new EditGuestWindow()
            {
                DataContext = viewModel
            };
            viewModel.OnUserDataChange += ViewModel_OnUserDataChange1; 
            window.ShowDialog();

            viewModel.OnUserDataChange -= ViewModel_OnUserDataChange1;
            viewModel = null;

        }

        private void ViewModel_OnUserDataChange1(object sender)
        {
            if (DataGrid.Any())
            {
                DataGrid.Clear();
            }

            DataGrid = ReturnGuests();
            
            
        }

    

        private void OnLoad()
        {
            
            
        }

        private ObservableCollection<Guest> ReturnGuests()
        {
            var guests = _guestSearchService.GetGuests();
          
          
            
            return new ObservableCollection<Guest>(guests);
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
                RaisePropertyChanged(nameof(DataGrid));
                MessageBox.Show("Uspesno upisan gost.", "Obavestenje", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           

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
        
    }
    
}
