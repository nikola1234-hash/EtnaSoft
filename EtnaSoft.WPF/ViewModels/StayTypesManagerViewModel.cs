using System.Windows.Automation;
using System.Windows.Input;
using DevExpress.Mvvm;
using EtnaSoft.Bll.Services;
using EtnaSoft.WPF.Events;
using Prism.Events;

namespace EtnaSoft.WPF.ViewModels
{
    public sealed class StayTypesManagerViewModel : EtnaBaseViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IStayTypesManagerService _stayTypesManager;
        private ICurrentWindowService CurrentWindowService => GetService<ICurrentWindowService>();
        private IDialogService DialogService => GetService<IDialogService>();
        public ICommand CloseWindowCommand { get; }
        public ICommand NewStayTypeCommand { get; }
        private readonly CreateStayTypeDialogViewModel createStayTypeDialogViewModel;
        public StayTypesManagerViewModel(IEventAggregator eventAggregator, IStayTypesManagerService stayTypesManager, CreateStayTypeDialogViewModel createStayTypeDialogViewModel)
        {
            _eventAggregator = eventAggregator;
            _stayTypesManager = stayTypesManager;
            this.createStayTypeDialogViewModel = createStayTypeDialogViewModel;
            CloseWindowCommand = new DelegateCommand(OnWindowClosing);
            NewStayTypeCommand = new DelegateCommand(OpenNewDialog);
        }
        //TODO: Finish the metod
        private void OpenNewDialog()
        {
            UICommand result = DialogService.ShowDialog(createStayTypeDialogViewModel.UICommands,
                "Kreiraj tip smestaja", viewModel: createStayTypeDialogViewModel);

        }

        //Gets called when Closing application
        private void OnWindowClosing()
        {
            _eventAggregator.GetEvent<WindowManagerOpenEvent>().Publish();
            CurrentWindowService?.Close();
        }


        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
