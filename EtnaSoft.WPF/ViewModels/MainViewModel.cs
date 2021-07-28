using DevExpress.Mvvm;
using System;
using System.Windows.Input;
using System.Windows.Media;
using EtnaSoft.WPF.Commands;
using EtnaSoft.WPF.Navigation;
using EtnaSoft.WPF.Services;
using EtnaSoft.WPF.Stores;

namespace EtnaSoft.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //Bound to MainWindow Grid Background
        public Brush BgColor { get; set; }

        private readonly IViewStore _viewStore;
        public ICommand NavigateCommand { get; }
        public MainViewModel(IViewStore viewStore, IEtnaViewModelFactory viewModelFactory)
        {
            _viewStore = viewStore;
            _viewStore.ViewChanged += OnViewChanged;
            NavigateCommand = new NavigateCommand(viewModelFactory, _viewStore);
            NavigateCommand.Execute(ViewType.LoginView);

            //TODO: When LogedIn Change the color to White or something like that
            BgColor = new SolidColorBrush(Colors.Black);
        }

        private void OnViewChanged()
        {
            RaisePropertiesChanged(nameof(CurrentViewModel));
        }

        public ViewModelBase CurrentViewModel => _viewStore.CurrentViewModel;


    }
}