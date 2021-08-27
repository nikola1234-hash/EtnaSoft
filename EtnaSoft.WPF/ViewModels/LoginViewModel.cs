using System;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Mvvm;
using EtnaSoft.Dal.Exceptions;
using EtnaSoft.WPF.Services.Authentication;
using EtnaSoft.WPF.Stores;

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
        public LoginViewModel(IAuthenticator authenticator, IRenavigate renavigate, IViewStore view)
        {
            _authenticator = authenticator;
            _renavigate = renavigate;
            _view = view;
            LoginCommand = new DelegateCommand<object>(OnLogin);
            ErrorMessageViewModel = new MessageViewModel();
            
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
