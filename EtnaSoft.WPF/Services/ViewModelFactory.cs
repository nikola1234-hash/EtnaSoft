using System;
using DevExpress.Mvvm;
using EtnaSoft.WPF.Navigation;
using EtnaSoft.WPF.ViewModels;

namespace EtnaSoft.WPF.Services
{
    public class ViewModelFactory : IEtnaViewModelFactory
    {
        private readonly LoginViewModel _loginView;
        private readonly HomeViewModel _homeView;
        private readonly ReceptionViewModel _receptionView;
        public ViewModelFactory(LoginViewModel loginView, HomeViewModel homeView, ReceptionViewModel receptionView)
        {
            _loginView = loginView;
            _homeView = homeView;
            _receptionView = receptionView;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.LoginView:
                    return _loginView;
                case ViewType.HomeView:
                    return _homeView;
                case ViewType.Reception:
                    return _receptionView;

                default:
                    throw new Exception("No valid views");

            }
        }
    }
}
