using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mvvm;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Services;

namespace EtnaSoft.WPF.ViewModels
{
    public sealed class EditStayTypeViewModel : BaseStayTypeDialogViewModel
    {
        private readonly ISpecialTypeService _specialTypeService;
        private readonly IStayTypesManagerService _stayTypesManagerService;
        private bool _isEnabled;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                RaisePropertyChanged(nameof(IsEnabled));
            }
        }
        public StayType StayType { get; set; }
        public ICommand LoadCommand { get; }
        public int PromotionId { get; set; }
        private ICurrentWindowService WindowService => GetService<ICurrentWindowService>();
        public ICommand ExecuteCommand { get; }
        public ICommand<CancelEventArgs> AbortCommand { get; }



     
        public EditStayTypeViewModel(StayType stayType, ISpecialTypeService specialTypeService, IStayTypesManagerService stayTypesManagerService)
        {
            StayType = stayType;
            _specialTypeService = specialTypeService;
            _stayTypesManagerService = stayTypesManagerService;
            LoadCommand = new DelegateCommand(OnLoad);
            ExecuteCommand = new DelegateCommand(Execute, CanExecute);
            AbortCommand = new DelegateCommand<CancelEventArgs>(Abort);
        }

        private void OnLoad()
        {
            PopulateForm();
        }

        void CloseWindow()
        {
            WindowService?.Close();
        }
        void PopulateForm()
        {
            Title = StayType.Title;
            Price = StayType.Price;
            if (StayType.IsSpecialType)
            {
                
                var specialType = _specialTypeService.GetPromotionByStayTypeId(StayType.Id);
                PromotionId = specialType.Id;
                IsEnabled = specialType != null;
                ChildPrice = specialType.ChildPrice;
                StayDays = specialType.StayDays;
            }
        }
        public override bool CanExecute()
        {
            bool canExecute;

            canExecute = PropertyDirty();

            return canExecute;
        }

        private bool PropertyDirty()
        {
            return IsDirty;
        }
        public override void Execute()
        {
            StayType st = new StayType()
            {
                Title = Title,
                Price = Price,
                IsActive = StayType.IsActive
            };
            _stayTypesManagerService.UpdateStayType(StayType.Id, st);
            if (StayType.IsSpecialType)
            {
                Promotion prom = new Promotion()
                {
                    ChildPrice = ChildPrice,
                    StayDays = StayDays,
                    Price = Price,
                    Id = PromotionId
                };
                _specialTypeService.UpdatePromotion(prom);
                
            }
            CloseWindow();
        }

        public override void Abort(CancelEventArgs obj)
        {
            CloseWindow();
        }

        public override void Dispose()
        {
            ClearFields();
            base.Dispose();
        }
    }
}
