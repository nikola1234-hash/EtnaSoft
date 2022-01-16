using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mvvm;
using EtnaSoft.Dal.Services.Authorization;
using EtnaSoft.WPF.Window;

namespace EtnaSoft.WPF.ViewModels
{
    public class UserContentViewModel : ContentViewModel
    {
        private readonly CreateUserViewModel _userViewModel;
        public ICommand CreateUserCommand { get; }

        public UserContentViewModel(CreateUserViewModel userViewModel)
        {
            _userViewModel = userViewModel;

            CreateUserCommand = new DelegateCommand(OnCreateUserCommand);
        }

        private void OnCreateUserCommand()
        {
            CreateUserWindow window = new CreateUserWindow()
            {
                DataContext = _userViewModel
            };

            window.Show();
        }

        
    }
}
