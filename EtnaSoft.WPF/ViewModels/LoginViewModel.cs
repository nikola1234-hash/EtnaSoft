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

namespace EtnaSoft.WPF.ViewModels
{
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

        private readonly IAuthenticator _authenticator;
        private readonly IRenavigate _renavigate;
        private readonly IViewStore _view;
        public ICommand LoadedCommand { get; }
        public LoginViewModel(IAuthenticator authenticator, IRenavigate renavigate, IViewStore view)
        {
            _authenticator = authenticator;
            _renavigate = renavigate;
            _view = view;
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
            
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                CurrentVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                try
                {
                    ApplicationDeployment.CurrentDeployment.UpdateAsync();
                    CurrentVersion = string.Empty;
                    CurrentVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4);
                }
                catch
                {
                    throw;
                }
               
            }

        }

        private void OnLogin(object obj)
        {
            if (obj is PasswordBox passwordBox)
            {
                var pass = passwordBox.Password;
                try
                {
                    var successLogin = _authenticator.Login(Username, pass);
                    if (successLogin)
                    {
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
            ErrorMessageViewModel?.Dispose();
            base.Dispose();
        }
    }
}
