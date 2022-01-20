using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Services;
using EtnaSoft.WPF.Events;
using EtnaSoft.WPF.Infrastructure;
using EtnaSoft.WPF.Window;
using Prism.Events;

namespace EtnaSoft.WPF.ViewModels
{
    public sealed class UserSubViewModel : EtnaBaseViewModel, IUserManagerSubViewModel
    {
        
        private readonly IUserManagerService _userManagerService;
        private readonly IEventAggregator _eventAggregator;
        public User User { get; set; }
        public string Username { get; set; }
        public ICommand<object> UserCommand { get; }
        public ICommand<object> RightMouseCommand { get; }
        public ICommand DeactivateAccountCommand { get; }
        public ICommand ActivateAccountCommand { get; }
        public bool IsActivateMenuVisible => !User.IsActive;

        public bool IsDeactivateMenuVisible => User.IsActive;


        public string IsActive
        {
            get
            {
                if (User.IsActive)
                {
                    return @"C:\InDevelopment\EtnaSoft\EtnaSoft.WPF\Icons\BO_Person.svg";
                }
                else
                {
                    return @"C:\InDevelopment\EtnaSoft\EtnaSoft.WPF\Icons\BO_Employee.svg";
                }
            }
        }
        public UserSubViewModel(IUserManagerService userManagerService, User user, IEventAggregator eventAggregator)
        {
            _userManagerService = userManagerService;
            UserCommand = new DelegateCommand<object>(OpenSingleUser);
            User = user;
            _eventAggregator = eventAggregator;

            Username = User.Username;
            RightMouseCommand = new DelegateCommand(OnRightMouseDown);
            DeactivateAccountCommand = new DelegateCommand(ActivateDeactivateUserAccount);
            ActivateAccountCommand = new DelegateCommand(ActivateDeactivateUserAccount);

        }

        private void ActivateDeactivateUserAccount()
        {
            _userManagerService.SetActiveOrInactiveUser(User.Id);
            _eventAggregator.GetEvent<UserStatusChangedEvent>().Publish();

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
