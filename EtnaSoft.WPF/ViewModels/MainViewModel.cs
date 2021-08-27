using DevExpress.Mvvm;
using System;
using System.Windows.Input;
using System.Windows.Media;
using EtnaSoft.WPF.Commands;
using EtnaSoft.WPF.Navigation;
using EtnaSoft.WPF.Services;
using EtnaSoft.WPF.Services.Authentication;
using EtnaSoft.WPF.Stores;

namespace EtnaSoft.WPF.ViewModels
{
    public class MainViewModel : EtnaBaseViewModel
    {
        //Bound to MainWindow Grid Background
        public Brush BgColor { get; set; }

        public bool IsLoggedIn => _authenticator.IsLoggedIn;
        private readonly IAuthenticator _authenticator;
        private readonly IViewStore _viewStore;
        public ICommand NavigateCommand { get; }
        public ICommand OnLoadCommand { get; }
        public MainViewModel(IViewStore viewStore, IEtnaViewModelFactory viewModelFactory, IAuthenticator authenticator)
        {
            _viewStore = viewStore;
            _authenticator = authenticator;
            _viewStore.ViewChanged += OnViewChanged;
            NavigateCommand = new NavigateCommand(viewModelFactory, _viewStore);
            OnLoadCommand = new DelegateCommand(OnLoadExecute);
        }

        private void OnLoadExecute()
        {
            NavigateCommand.Execute(ViewType.LoginView);
            
            //TODO: When LogedIn Change the color to White or something like that
            BgColor = new SolidColorBrush(Colors.Black);
        }

        private void OnViewChanged()
        {
            RaisePropertiesChanged(nameof(CurrentViewModel));
            RaisePropertiesChanged(nameof(IsLoggedIn));
        }

        public EtnaBaseViewModel CurrentViewModel => _viewStore.CurrentViewModel;
        public override void Dispose()
        {
            _viewStore.ViewChanged -= OnViewChanged;
            base.Dispose();
        }
    }
}