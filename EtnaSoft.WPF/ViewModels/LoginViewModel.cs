using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using EtnaSoft.WPF.Commands;

namespace EtnaSoft.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
            LoginCommand = new LoginCommand();
        }


        public ICommand LoginCommand { get; }
        public ICommand CloseCommand { get; }
    }
}
