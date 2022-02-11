using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Mvvm;
using EtnaSoft.Dal.Exceptions;
using EtnaSoft.WPF.Services.Authentication;
using EtnaSoft.WPF.Stores;
using Squirrel;
using System.Deployment.Application;
using System.Threading;
using System.Windows;
using EtnaSoft.Dal.Services.Authorization;
using Serilog;
using Microsoft.Extensions.Logging;

namespace EtnaSoft.WPF.ViewModels
{
    public enum DefaultAccount
    {
        Admin = 1,
        admin = 2

    }
    public class LoginViewModel : EtnaBaseViewModel
    {



        private string _username;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                RaisePropertiesChanged(nameof(Username));
            }
        }
        public MessageViewModel ErrorMessageViewModel { get; set; }
        private string _currentVersion;

        public string CurrentVersion
        {
            get { return _currentVersion; }
            set
            {
                _currentVersion = value;
                RaisePropertyChanged(nameof(CurrentVersion));
            }
        }

        private bool _updateFlag;
        private string _availableVersion;

        public string AvailableVersion
        {
            get { return _availableVersion; }
            set
            {
                _availableVersion = value;
                RaisePropertyChanged(nameof(AvailableVersion));
            }
        }
        public string ErrorMessage
        {
            set
            {
                ErrorMessageViewModel.Message = value;
            }
        }
        private IDialogService DialogService
        {
            get => GetService<IDialogService>();
        }

      

        private readonly IAuthenticator _authenticator;
        private readonly IAuthorization _auth;
        private readonly IRenavigate _renavigate;
        private readonly IViewStore _view;
        private readonly ILogger<LoginViewModel> _logger;
        public ICommand LoadedCommand { get; }
        public LoginViewModel(IAuthenticator authenticator, IRenavigate renavigate, IViewStore view, ILogger<LoginViewModel> logger, IAuthorization auth)
        {
            
            _authenticator = authenticator;
            _renavigate = renavigate;
            _view = view;
            _logger = logger;
            _auth = auth;
            LoginCommand = new DelegateCommand<object>(OnLogin);
            ErrorMessageViewModel = new MessageViewModel();
            LoadedCommand = new DelegateCommand(OnViewLoad);
            
        }

       

        private async void OnViewLoad()
        {
            await CheckForUpdates();
        }
        //TODO: Update online for continuous integration
        private async Task CheckForUpdates()
        {
            //TODO: Update App Logic Here
            
        }

        private void OnLogin(object secret)
        {

            bool IsDefaultAccount(string pass)
                {
                    return Username == DefaultAccount.Admin.ToString() &&
                           pass == DefaultAccount.admin.ToString();
                }

                void ChangePasswordDialog(string pass)
                {
                    ChangePasswordDialogViewModel changePassword = new ChangePasswordDialogViewModel(_username, pass, _auth);
                    UICommand result = DialogService.ShowDialog(changePassword.Commands, "Promeni lozinku",
                        viewModel: changePassword);
                    if (result != null)
                    {
                        MessageBox.Show("Uspesno promenjena lozinka");
                    }
                }

                if (secret is PasswordBox passwordBox)
                {
                    var pass = passwordBox.Password;
                    
                    try
                    {
                        var successLogin = _authenticator.Login(_username, pass);
                        if (successLogin)
                        {
                            if (IsDefaultAccount(pass))
                            {
                                _logger.LogInformation(
                                    "This account is default one, popping modal for password change");
                                ChangePasswordDialog(pass);
                                _logger.LogInformation("Password change done.");
                            }

                            _view.CurrentViewModel = _renavigate.Navigate();

                        }
                    }
                    catch (UserNotActiveExeption)
                    {
                        ErrorMessage = "Ovaj korisnik je neaktivan";
                    }
                    catch (Exception)
                    {
                        ErrorMessage = "Pogresni parametri za logovanje";
                    }
                }
    
            


        }


        public ICommand LoginCommand { get; }
        public ICommand CloseCommand { get; }
        public override void Dispose()
        {
            
            base.Dispose();
            ErrorMessageViewModel?.Dispose();
        }
    }
}
