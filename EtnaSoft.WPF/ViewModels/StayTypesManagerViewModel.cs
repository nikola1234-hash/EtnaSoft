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
        private readonly CreateStayTypeDialogViewModel createStayTypeDialogViewModel;

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
        public StayTypesManagerViewModel(IEventAggregator eventAggregator, IStayTypesManagerService stayTypesManager, CreateStayTypeDialogViewModel createStayTypeDialogViewModel)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<StayTypeStatusChangedEvent>().Subscribe(LoadStayTypes);
            _stayTypesManager = stayTypesManager;
            this.createStayTypeDialogViewModel = createStayTypeDialogViewModel;
            CloseWindowCommand = new DelegateCommand(OnWindowClosing);
            NewStayTypeCommand = new DelegateCommand(OpenNewDialog);
            LoadCommand = new DelegateCommand(OnLoad);
        }

        private void OnLoad()
        {
            LoadStayTypes();
        }

        //TODO: Finish the metod
        private void OpenNewDialog()
        {
            UICommand result = DialogService.ShowDialog(createStayTypeDialogViewModel.UICommands,
                "Kreiraj tip smestaja", viewModel: createStayTypeDialogViewModel);
            if ((MessageResult)result.Id == MessageResult.OK)
            {
                LoadStayTypes();
            }

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
