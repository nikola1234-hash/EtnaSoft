using DevExpress.Mvvm;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using EtnaSoft.Dal.Stores;
using EtnaSoft.WPF.Commands;
using EtnaSoft.WPF.Navigation;
using EtnaSoft.WPF.Services;
using EtnaSoft.WPF.Services.Authentication;
using EtnaSoft.WPF.Stores;
using Squirrel;

namespace EtnaSoft.WPF.ViewModels
{
    public class MainViewModel : EtnaBaseViewModel
    {
        //Bound to MainWindow Grid Background
        public Brush BgColor { get; set; }

        public bool IsLoggedIn => _authenticator.IsLoggedIn;
        public string CurrentUser => UserStore.CurrentUser;
        private readonly IAuthenticator _authenticator;
        private readonly IViewStore _viewStore;
        public ICommand NavigateCommand { get; }
        public ICommand OnLoadCommand { get; }
        public ICommand LogoutCommand { get; }
        public MainViewModel(IViewStore viewStore, IEtnaViewModelFactory viewModelFactory, IAuthenticator authenticator)
        {
            _viewStore = viewStore;
            _authenticator = authenticator;
            _viewStore.ViewChanged += OnViewChanged;
            NavigateCommand = new NavigateCommand(viewModelFactory, _viewStore);
            OnLoadCommand = new DelegateCommand(OnLoadExecute);
            LogoutCommand = new DelegateCommand(Logout);

        }

        private void Logout()
        {
            if(CurrentUser != null)
                _authenticator.Logout();
            
            RaisePropertyChanged(nameof(IsLoggedIn));
            RaisePropertyChanged(nameof(CurrentUser));
            if (!IsLoggedIn)
            {
                NavigateToLoginView();
            }
        }

        private void NavigateToLoginView()
        {
            NavigateCommand.Execute(ViewType.LoginView);
        }
        private void OnLoadExecute()
        {
            NavigateToLoginView();
            
            //TODO: When LogedIn Change the color to White or something like that
            BgColor = new SolidColorBrush(Colors.Black);
        }

        private void OnViewChanged()
        {
            RaisePropertiesChanged(nameof(CurrentViewModel));
            RaisePropertiesChanged(nameof(IsLoggedIn));
            RaisePropertyChanged(nameof(CurrentUser));
        }

        public EtnaBaseViewModel CurrentViewModel => _viewStore.CurrentViewModel;
        public override void Dispose()
        {
            _viewStore.ViewChanged -= OnViewChanged;
            base.Dispose();
        }
    }
}