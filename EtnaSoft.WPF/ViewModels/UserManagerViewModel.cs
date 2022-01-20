using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DevExpress.Mvvm;
using EtnaSoft.Bll.Services;
using EtnaSoft.WPF.Events;
using EtnaSoft.WPF.Services;
using Prism.Events;

namespace EtnaSoft.WPF.ViewModels
{
    public sealed class UserManagerViewModel : EtnaBaseViewModel
    {
        private ObservableCollection<UserSubViewModel> _userCollection;

        public ObservableCollection<UserSubViewModel> UserCollection
        {
            get { return _userCollection; }
            set
            {
                _userCollection = value;
                RaisePropertyChanged(nameof(UserCollection));
            }
        }

        private readonly IUserManagerService _userManager;
        
        private ICurrentWindowService CurrentWindowService => this.GetService<ICurrentWindowService>();
        private INotificationService NotificationService => GetService<INotificationService>();
        private readonly ICreateUserViewFactory createUserViewFactory;
        private readonly IEventAggregator _eventAggregator;
        public ICommand OnLoadedCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand<WindowType> NewUserCommand { get; }

        public static string ApplicationId
        {
            get
            {
                return "UserManager";
            }
        }

        
        public string ActiveUser
        {
            get
            {
                return "dx:DXImage SvgImages/Outlook Inspired/Customers.svg";
            }
        }
        public bool IsCreateUserWindowOpen { get; set; }
        public UserManagerViewModel(IUserManagerService userManager, ICreateUserViewFactory createUserViewFactory, IEventAggregator eventAggregator)
        {
            _userManager = userManager;
            this.createUserViewFactory = createUserViewFactory;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<UserCreatedEvent>().Subscribe(OnUserCreation);
            _eventAggregator.GetEvent<UserStatusChangedEvent>().Subscribe(RepopulateUsers);

            OnLoadedCommand = new DelegateCommand(OnLoad);
            CloseCommand = new DelegateCommand(OnClosing);
            NewUserCommand = new DelegateCommand<WindowType>(OpenNewUserWindow);

        }

        private void OnUserCreation()
        {
            RepopulateUsers();
            ShowNotification("Uspesno dodat novi korisnik");
        }

        private void ShowNotification(string message)
        {
            
            INotification notification =
                NotificationService.CreatePredefinedNotification("Obavestenje", message,
                    DateTime.Now.ToShortDateString());
            notification.ShowAsync();
        }
        private void OpenNewUserWindow(WindowType windowType)
        {
            if (IsCreateUserWindowOpen)
            {
                return;
            }
       
            var window = createUserViewFactory.CreateView(windowType);
            window.Show();
            IsCreateUserWindowOpen = window.IsActive;
            _eventAggregator.GetEvent<UserCreationWindowCloseEvent>().Subscribe(SetCreateUserWindowClose);
        }

        void SetCreateUserWindowClose()
        {
            IsCreateUserWindowOpen = false;
            _eventAggregator.GetEvent<UserCreationWindowCloseEvent>().Unsubscribe(SetCreateUserWindowClose);
        }

        private void CloseWindow()
        {
            CurrentWindowService.Close();
        }
        private void OnClosing()
        {
            _eventAggregator.GetEvent<UserManagerOpenEvent>().Publish();
            CloseWindow();
        }
        void RepopulateUsers()
        {
            UserCollection?.Clear();
            UserCollection = null;
            UserCollection = new ObservableCollection<UserSubViewModel>();
            var users = _userManager.GetAllUsers().ToList();
            foreach (var user in users)
            {
                UserCollection.Add(new UserSubViewModel(_userManager, user, _eventAggregator));
            }
            
        }
        private void OnLoad()
        {
            UserCollection = new ObservableCollection<UserSubViewModel>();
            var users = _userManager.GetAllUsers().ToList();
            foreach (var user in users)
            {
                UserCollection.Add(new UserSubViewModel(_userManager, user, _eventAggregator));
            }

            

        }

        public override void Dispose()
        {
            UserCollection.Clear();
            UserCollection = null;
            _eventAggregator.GetEvent<UserCreatedEvent>().Unsubscribe(OnUserCreation);
            base.Dispose();
          
        }
        
    }
}