using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Mvvm;

namespace EtnaSoft.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public MessageViewModel ErrorMessageViewModel { get; set; }

        public string ErrorMessage
        {
            set
            {
                ErrorMessageViewModel.Message = value;
            }
        }

        public LoginViewModel()
        {
            LoginCommand = new DelegateCommand<object>(OnLogin);
            ErrorMessageViewModel = new MessageViewModel();
            
        }

        private void OnLogin(object obj)
        {
            if (obj is PasswordBox passwordBox)
            {
                var pass = passwordBox.Password;
                ErrorMessage = "Error 123";
            }
        }


        public ICommand LoginCommand { get; }
        public ICommand CloseCommand { get; }
    }
}
