using EtnaSoft.WPF.Navigation;
using EtnaSoft.WPF.Services;
using EtnaSoft.WPF.Stores;

namespace EtnaSoft.WPF.Commands
{
    public class NavigateCommand : BaseCommand
    {
        private readonly IEtnaViewModelFactory _viewModelFactory;
        private readonly IViewStore _viewStore;
        public NavigateCommand(IEtnaViewModelFactory viewModelFactory, IViewStore viewStore)
        {
            _viewModelFactory = viewModelFactory;
            _viewStore = viewStore;
        }

        public override void Execute(object parameter)
        {
            if (parameter is ViewType viewType)
            {
                _viewStore.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
            }
        }
    }
}
