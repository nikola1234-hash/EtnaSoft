using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using EtnaSoft.WPF.Navigation;
using EtnaSoft.WPF.ViewModels;

namespace EtnaSoft.WPF.Services
{
    public class ViewModelFactory : IEtnaViewModelFactory
    {
        private readonly LoginViewModel _loginView;
        private readonly HomeViewModel _homeView;

        public ViewModelFactory(LoginViewModel loginView, HomeViewModel homeView)
        {
            _loginView = loginView;
            _homeView = homeView;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.LoginView:
                    return _loginView;
                case ViewType.HomeView:
                    return _homeView;
                

                default:
                    throw new Exception("No valid views");

            }
        }
    }
}
