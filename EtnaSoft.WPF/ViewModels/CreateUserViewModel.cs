using System.Windows.Input;
using DevExpress.Mvvm;
using EtnaSoft.Dal.Services.Authorization;
using EtnaSoft.Dal.Services.UserServices;



namespace EtnaSoft.WPF.ViewModels
{
    public sealed class CreateUserViewModel : ContentViewModel
    {
        private readonly IAuthorization _authorization;

        private UserService userService;
        public ICommand CreateCommand { get; }
        public IMessageBoxService MessageService
        {
            get { return this.GetService<IMessageBoxService>(); }
        }
        public CreateUserViewModel(IAuthorization authorization)
        {
            _authorization = authorization;
            CreateCommand = new DelegateCommand(OnUserCreate);
        }

        private void OnUserCreate()
        {
            var result = _authorization.RegisterUser(FirstName, LastName, Username, Password, RepeatPassword);
            if (result == RegistrationStatus.Success)
            {
                MessageService.Show($"Uspesno kreiran korisnik: {Username}");
            }
            else if(result == RegistrationStatus.UsernameAlreadyExists)
            {
                MessageService.Show($"Korisnik sa ovim imenom vec postoji");

            }
        }


        public bool PasswordsMatch => Password == RepeatPassword;


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

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                var result = userService.DoesUserExists(s => s.Username == _username);
                if (result)
                {
                    MessageService.Show($"Korisnik sa ovim imenom vec postoji u bazi!");
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
            }
        }
    }
}
