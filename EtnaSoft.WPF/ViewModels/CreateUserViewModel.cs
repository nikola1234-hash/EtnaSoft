using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using EtnaSoft.Dal.Services.Authorization;
using EtnaSoft.Dal.Services.UserServices;
using EtnaSoft.Dal.Stores;
using EtnaSoft.WPF.Events;
using EtnaSoft.WPF.Helpers;
using Prism.Events;

namespace EtnaSoft.WPF.ViewModels
{
    public sealed class CreateUserViewModel : ContentViewModel, IDataErrorInfo
    {
        private readonly IAuthorization _authorization;
        public MessageViewModel ErrorMessageViewModel { get; set; }
        private readonly IUserService userService;
        public ICommand CreateCommand { get; }
        public ICommand CloseCommand { get; }
        private readonly IEventAggregator _eventAggregator;
        public IMessageBoxService MessageService
        {
            get { return this.GetService<IMessageBoxService>(); }
        }

   
        public ICurrentWindowService WindowService => this.GetService<ICurrentWindowService>();
        public CreateUserViewModel(IAuthorization authorization, IUserService userService, IEventAggregator eventAggregator)
        {
            _authorization = authorization;
            this.userService = userService;
            _eventAggregator = eventAggregator;
            CreateCommand = new DelegateCommand(OnUserCreate);
            CloseCommand = new DelegateCommand(OnClose);

            ErrorMessageViewModel = new MessageViewModel();

        }

        private void OnClose()
        {
            ErrorMessageViewModel.Dispose();
            CloseWindow();
        }
        private void OnUserCreate()
        {
            var validation = EnableValidationAndGetError();
            if (validation != null)
                return;
            try
            {
                var result = _authorization.RegisterUser(FirstName, LastName, Username, Password, RepeatPassword);
                if (result == RegistrationStatus.Success)
                {
                   
                    MessageService.Show(
                            $"Uspesno kreiran korisnik: {Username}. Da li zelite da unesete novi zapis?", "Obavestenje");
                    
                    CloseWindow();
                }
                else if (result == RegistrationStatus.UsernameAlreadyExists)
                {
                    MessageService.Show($"Korisnik sa ovim imenom vec postoji");

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        //If User is not admin disable form
        public bool IsFormEnabled => UserStore.CurrentUser == "Admin";
        void CloseWindow()
        {
            _eventAggregator.GetEvent<UserCreationWindowCloseEvent>().Publish();
            this.WindowService.Close();
        }
        /// <summary>
        /// This method uses reflectionn on this class to clear all
        /// string fields.
        /// </summary>
        void ClearFields()
        {
            //var properties = this.GetType().GetProperties();
            //foreach (var property in properties)
            //{
            //    if (property.PropertyType == typeof(string))
            //    {
            //        property.SetValue(this, string.Empty, null);
            //    }
            //}
        }

        bool PasswordNotEmptyAndMatch()
        {
            return !string.IsNullOrWhiteSpace(Password)
                   && !string.IsNullOrWhiteSpace(RepeatPassword)
                   && Password == RepeatPassword;
        }

        private bool _passwordsMatch;

        public bool PasswordsMatch
        {
            get { return _passwordsMatch; }
            set
            {
                _passwordsMatch = value;
                RaisePropertyChanged(nameof(PasswordsMatch));
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

        private string _username;

        public string ErrorMessage
        {
            set
            {
                ErrorMessageViewModel.Message = value;
            }
        }
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                var result = userService.DoesUserExists(s => s.Username == _username);
                if (result)
                {
                    MessageService.Show($"Ovo Korisnicko ime postoji u bazi, izaberite drugo");
                    _username = string.Empty;
                    RaisePropertyChanged(nameof(Username));

                    return;
                }
                RaisePropertyChanged(nameof(Username));
            }
        }
        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }

        private string _repeatPassword;

        public string RepeatPassword
        {
            get { return _repeatPassword; }
            set
            {
                _repeatPassword = value;
                RaisePropertyChanged(nameof(RepeatPassword));
                if (Password == RepeatPassword)
                {
                    PasswordsMatch = true;
                    
                }
                else
                {
                    PasswordsMatch = false;
                }
                RaisePropertyChanged(nameof(PasswordsMatch));
            }
        }

        private string EnableValidationAndGetError()
        {
            string error =((IDataErrorInfo)this).Error;
            if (!string.IsNullOrEmpty(error))
            {
                this.RaisePropertiesChanged();
                return error;
            }

            return null;
        }

        public string this[string columnName]
        {
            get
            {
                string firstName = BindableBase.GetPropertyName(() => FirstName);
                string lastName = BindableBase.GetPropertyName(() => LastName);
                string username = BindableBase.GetPropertyName(() => Username);
                string password = BindableBase.GetPropertyName(() => Password);
                string repeatPassword = BindableBase.GetPropertyName(() => RepeatPassword);

                if (columnName == firstName)
                {
                    return RequiredValidationRule.GetErrorMessage(firstName, FirstName);
                }
                else if (columnName == lastName)
                {
                    return RequiredValidationRule.GetErrorMessage(lastName, LastName);
                }
                else if (columnName == username)
                {
                    return RequiredValidationRule.GetErrorMessage(username, Username);
                }
                else if (columnName == password)
                {
                    return RequiredValidationRule.GetErrorMessage(password, Password);
                }
                else if (columnName == repeatPassword)
                {
                    return RequiredValidationRule.GetErrorMessage(repeatPassword, RepeatPassword);
                }
                return null;
            }
        }

        public string Error
        {
            get
            {
                IDataErrorInfo me = (IDataErrorInfo)this;
                string errors = me[BindableBase.GetPropertyName(() => FirstName)] +
                                me[BindableBase.GetPropertyName(() => LastName)] +
                                me[BindableBase.GetPropertyName(() => Username)] +
                                me[BindableBase.GetPropertyName(() => Password)] +
                                me[BindableBase.GetPropertyName(() => RepeatPassword)];
                if (!string.IsNullOrEmpty(errors))
                    return "Ova polja moraju biti popunjena";

                return null;
            }
        }
        
        
    }
}
