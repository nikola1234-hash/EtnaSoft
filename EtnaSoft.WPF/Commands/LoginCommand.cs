using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EtnaSoft.WPF.Commands
{
    public class LoginCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            if (parameter is PasswordBox passwordBox)
            {
                string password = passwordBox.Password;
            }
        }
    }
}
