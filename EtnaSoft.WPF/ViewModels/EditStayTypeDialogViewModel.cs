using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mvvm;
using EtnaSoft.Bll.Services;

namespace EtnaSoft.WPF.ViewModels
{
    public sealed class EditStayTypeDialogViewModel : BaseStayTypeDialogViewModel
    {
        public int Id { get; set; }
        private UICommand UpdateCommand => 
            new UICommand(id: MessageResult.OK, "Izmeni",
            command: new DelegateCommand(Execute, CanExecute),
            isDefault: true,
            isCancel: false);

        private UICommand AbortCommand =>
            new UICommand(id: MessageResult.No, command: new DelegateCommand<CancelEventArgs>(Abort),
                isDefault: false,
                isCancel: true,
                caption: "Nazad");

        public List<UICommand> Commands => new List<UICommand>();

        private readonly IStayTypesManagerService _stayTypesManagerService;
        private readonly ISpecialTypeService _speaSpecialTypeService;
        public ICommand OnLoadCommand { get; }
        public EditStayTypeDialogViewModel(IStayTypesManagerService stayTypesManagerService, ISpecialTypeService speaSpecialTypeService)
        {
            _stayTypesManagerService = stayTypesManagerService;
            _speaSpecialTypeService = speaSpecialTypeService;
            OnLoadCommand = new DelegateCommand(OnLoad);

            Commands.Add(UpdateCommand);
            Commands.Add(AbortCommand);
        }

        private void OnLoad()
        {
            PopulateForm();
        }

        public override bool CanExecute()
        {
            throw new NotImplementedException();
        }

        private void PopulateForm()
        {
            var stayType = _stayTypesManagerService.GetStayTypeById(Id);
            Title = stayType.Title;
            Price = stayType.Price;
            if (stayType.IsSpecialType)
            {
                IsSpecialType = stayType.IsSpecialType;
                var specialStayType = _speaSpecialTypeService.GetPromotionByStayTypeId(stayType.Id);
                ChildPrice = specialStayType.ChildPrice;
                StayDays = specialStayType.StayDays;
            }
        }
        public override void Execute()
        {
           
        }

        public override void Abort(CancelEventArgs obj)
        {
            obj.Cancel = false;
        }

        public override void Dispose()
        {

            base.Dispose();
        }
    }
}
