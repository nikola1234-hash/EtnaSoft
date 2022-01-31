using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using System.Windows.Documents;
using System.Windows.Input;
using DevExpress.Mvvm;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Services;
using EtnaSoft.WPF.Events;
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
                if (StayType.IsSpecialType)
                {
                    color = new SolidColorBrush(Colors.Coral);
                }
                else if (!StayType.IsActive)
                {
                    color =  new SolidColorBrush(Colors.Gray);
                }
                else if (StayType.IsSpecialType && !StayType.IsActive)
                {
                    color = new SolidColorBrush(Colors.Gray);
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
        public SubStayTypeViewModel(StayType stayType, IStayTypesManagerService stayTypesManager, IEventAggregator eventAggregator)
        {
            StayType = stayType;
            _stayTypesManager = stayTypesManager;
            _eventAggregator = eventAggregator;

            ActivateCommand = new DelegateCommand(ActivateOrDeactivateType);
            DeactivateCommand = new DelegateCommand(ActivateOrDeactivateType);

        }

        private void ActivateOrDeactivateType()
        {
            _stayTypesManager.DeactiveTypeOrActivate(StayType.Id);
            _eventAggregator.GetEvent<StayTypeStatusChangedEvent>().Publish();
        }
    }
}
