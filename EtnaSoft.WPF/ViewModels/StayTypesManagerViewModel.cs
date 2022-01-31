using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
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

        public string WindowTitle => "Podesavanje Smestaja";


        private readonly IEventAggregator _eventAggregator;
        private readonly IStayTypesManagerService _stayTypesManager;
        private ICurrentWindowService CurrentWindowService => GetService<ICurrentWindowService>();
        private IDialogService DialogService => GetService<IDialogService>();
        public ICommand LoadCommand { get; }
        public ICommand CloseWindowCommand { get; }
        public ICommand NewStayTypeCommand { get; }
        public ICommand<int> EditCommand { get; }
        private readonly CreateStayTypeDialogViewModel _createStayTypeDialogViewModel;
        private readonly EditStayTypeDialogViewModel _editStayTypeDialogViewModel;

        private ObservableCollection<SubStayTypeViewModel> _stayTypesCollection;

        public ObservableCollection<SubStayTypeViewModel> StayTypesCollection
        {
            get { return _stayTypesCollection; }
            set
            {
                _stayTypesCollection = value;
                RaisePropertyChanged(nameof(StayTypesCollection));
            }
        }

        private void LoadStayTypes()
        {
            if (StayTypesCollection == null)
            {
                StayTypesCollection = new ObservableCollection<SubStayTypeViewModel>();
            }

            if (StayTypesCollection.Any())
            {
                StayTypesCollection.Clear();
            }
            var types = _stayTypesManager.GetAllStayTypes();
            foreach (var type in types)
            {
                StayTypesCollection.Add(new SubStayTypeViewModel(type, _stayTypesManager, _eventAggregator));
            }
        }
        public StayTypesManagerViewModel(IEventAggregator eventAggregator, IStayTypesManagerService stayTypesManager, CreateStayTypeDialogViewModel createStayTypeDialogViewModel, EditStayTypeDialogViewModel editStayTypeDialogViewModel)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<StayTypeStatusChangedEvent>().Subscribe(LoadStayTypes);
            _stayTypesManager = stayTypesManager;
            _createStayTypeDialogViewModel = createStayTypeDialogViewModel;
            _editStayTypeDialogViewModel = editStayTypeDialogViewModel;
            CloseWindowCommand = new DelegateCommand(OnWindowClosing);
            NewStayTypeCommand = new DelegateCommand(OpenNewDialog);
            LoadCommand = new DelegateCommand(OnLoad);
            EditCommand = new DelegateCommand<int>(OpenEditDialog);
        }

        private void OnLoad()
        {
            LoadStayTypes();
        }




        private void OpenEditDialog(int id)
        {
            _editStayTypeDialogViewModel.Id = id;
            UICommand result = DialogService.ShowDialog(_editStayTypeDialogViewModel.Commands, "Izmeni zapis",
                _editStayTypeDialogViewModel);
            if ((MessageResult)result.Id == MessageResult.OK)
            {
                LoadStayTypes();
            }
            _editStayTypeDialogViewModel.Dispose();
        }
        private void OpenNewDialog()
        {
            UICommand result = DialogService.ShowDialog(_createStayTypeDialogViewModel.UICommands,
                "Kreiraj tip smestaja", viewModel: _createStayTypeDialogViewModel);
            if ((MessageResult)result.Id == MessageResult.OK)
            {
                LoadStayTypes();
            }
            _createStayTypeDialogViewModel.Dispose();

        }

        //Gets called when Closing application
        private void OnWindowClosing()
        {
            _eventAggregator.GetEvent<WindowManagerOpenEvent>().Publish();
            CurrentWindowService?.Close();
        }


        public override void Dispose()
        {
            _eventAggregator.GetEvent<StayTypeStatusChangedEvent>().Unsubscribe(LoadStayTypes);
            base.Dispose();
        }
    }
}
