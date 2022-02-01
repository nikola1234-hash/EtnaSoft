using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using System.Windows.Documents;
using System.Windows.Input;
using DevExpress.Mvvm;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Services;
using EtnaSoft.WPF.Events;
using EtnaSoft.WPF.Window;
using Prism.Events;

namespace EtnaSoft.WPF.ViewModels
{
    public sealed class SubStayTypeViewModel : EtnaBaseViewModel
    {
        private StayType _stayType;

        public StayType StayType
        {
            get { return _stayType; }
            set
            {
                _stayType = value;
                if (_stayType.IsActive)
                {
                    IsDeactivateMenuVisible = true;
                    IsActivateMenuVisible = false;
                }
                else
                {
                    IsDeactivateMenuVisible = false;
                    IsActivateMenuVisible = true;
                }
                RaisePropertyChanged(nameof(StayType));
            }
        }
        public string Image => @"C:\InDevelopment\EtnaSoft\EtnaSoft.WPF\Icons\staytype.png";
        private bool _isActivateMenuVisible;

        private bool _isDeactivateMenuVisible;

        public bool IsDeactivateMenuVisible
        {
            get { return _isDeactivateMenuVisible; }
            set
            {
                _isDeactivateMenuVisible = value;
                RaisePropertyChanged(nameof(IsDeactivateMenuVisible));
            }
        }
        public bool IsActivateMenuVisible
        {
            get { return _isActivateMenuVisible; }
            set
            {
                _isActivateMenuVisible = value;
                RaisePropertyChanged(nameof(IsActivateMenuVisible));
            }
        }
        public Brush BackgroundColor
        {
            get
            {
                SolidColorBrush color;
                if (StayType.IsSpecialType && !StayType.IsActive)
                {
                    color = new SolidColorBrush(Colors.Gray);
                }
               
                else if (!StayType.IsActive)
                {
                    color =  new SolidColorBrush(Colors.Gray);
                }
                else if (StayType.IsSpecialType)
                {
                    color = new SolidColorBrush(Colors.Coral);
                }
                else
                {
                    color =  new SolidColorBrush(Colors.Green);
                }

                return color;
            }
        }
        public ICommand ActivateCommand { get; }
        public ICommand DeactivateCommand { get; }
        private readonly IStayTypesManagerService _stayTypesManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly ISpecialTypeService _specialTypeService;

        //TODO: COntext menu edit command 
        public ICommand EditCommand { get; }
        public SubStayTypeViewModel(StayType stayType, IStayTypesManagerService stayTypesManager, IEventAggregator eventAggregator, ISpecialTypeService specialTypeService)
        {
            StayType = stayType;
            _stayTypesManager = stayTypesManager;
            _eventAggregator = eventAggregator;
            _specialTypeService = specialTypeService;
            EditCommand = new DelegateCommand(OpenEditWindow);
            ActivateCommand = new DelegateCommand(ActivateOrDeactivateType);
            DeactivateCommand = new DelegateCommand(ActivateOrDeactivateType);
        }

        private void OpenEditWindow()
        {
            var viewModel = new EditStayTypeViewModel(StayType, _specialTypeService, _stayTypesManager);
            var window = new EditStayTypeWindow
            {
                DataContext = viewModel
            };
            window.ShowDialog();
            OnDataChange();
            viewModel.Dispose();
            
            
        }

        void OnDataChange()
        {
            _eventAggregator.GetEvent<StayTypeStatusChangedEvent>().Publish();
        }
        private void ActivateOrDeactivateType()
        {
            _stayTypesManager.DeactiveTypeOrActivate(StayType.Id);
            OnDataChange();
        }
    }
}
