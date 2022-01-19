using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Mvvm;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Services;
using EtnaSoft.WPF.Infrastructure;
using EtnaSoft.WPF.Window;

namespace EtnaSoft.WPF.ViewModels
{
    public sealed class UserSubViewModel : EtnaBaseViewModel, IUserManagerSubViewModel
    {
        private readonly IUserManagerService _userManagerService;
        public User User { get; set; }
        public string Username { get; set; }
        public ICommand<object> UserCommand { get; }
        public ICommand<object> RightMouseCommand { get; }
        public UserSubViewModel(IUserManagerService userManagerService, User user)
        {
            _userManagerService = userManagerService;
            UserCommand = new DelegateCommand<object>(OpenSingleUser);
            User = user;
            Username = User.Username;
            RightMouseCommand = new DelegateCommand(OnRightMouseDown);
        }

        private void OnRightMouseDown()
        {
            
        }

        private void OpenSingleUser(object obj)
        {
            var window = new UserDetailsWindow
            {
                DataContext = new UserDetailsViewModel(User)
            };
            window.Show();
        }

        

        public override void Dispose()
        {
            User = null;
            base.Dispose();
        }
    }
}
