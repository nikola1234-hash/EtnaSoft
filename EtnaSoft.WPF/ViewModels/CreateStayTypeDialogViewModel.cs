using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Dto;
using EtnaSoft.Bll.Services;

namespace EtnaSoft.WPF.ViewModels
{
    public sealed class CreateStayTypeDialogViewModel : BaseStayTypeDialogViewModel
    {
        private readonly ISpecialTypeService _specialTypeService;
        public List<UICommand> UICommands { get; set; }

        protected UICommand RegisterCommand
        {
            get; set; 

        }
        protected UICommand CancelCommand { get; set; }
        public ICommand OnLoadCommand { get; set; }

        public CreateStayTypeDialogViewModel(ISpecialTypeService specialTypeService)
        {
            _specialTypeService = specialTypeService;

            OnLoadCommand = new DelegateCommand(OnLoad);
            RegisterCommand = new UICommand()
            {
                Id= MessageResult.OK,
                IsDefault = true,
                Command = new DelegateCommand(Execute, CanExecute),
                Caption = "Registruj",
                IsCancel = false,

            };
            CancelCommand = new UICommand()
            {
                Id = MessageResult.Cancel,
                IsCancel = true,
                IsDefault = false,
                Caption = "Nazad",
                Command = new DelegateCommand<CancelEventArgs>(Abort)
            };
            UICommands = new List<UICommand>();
            UICommands.Add(RegisterCommand);
            UICommands.Add(CancelCommand);
        }

        private void OnLoad()
        {
            return;
        }


        public override bool CanExecute()
        {
            //Logic for can Execute for now is true
            return !string.IsNullOrEmpty(Title) && Price > 0;
        }

        public override void Execute()
        {
             
            try
            {
                StayTypesDto stayType = new StayTypesDto()
                {
                    Title = Title,
                    Price = Price,
                    ChildPrice = ChildPrice,
                    StayDays = StayDays,
                    IsSpecialType = IsSpecialType
                };
                _specialTypeService.Register(stayType);
            }
            catch
            {
                throw;
            }
            finally
            {
                MessageBox.Show("Uspesno kreiran zapis");
            }
        }

        public override void Abort(CancelEventArgs obj)
        {
            obj.Cancel = false;
        }

        public override void Dispose()
        {
            this.Title = string.Empty;
            this.Price = 0;
            this.ChildPrice = 0;
            this.StayDays = 0;
            this.CenaLabel = string.Empty;
            this.RegisterCommand = null;
            this.CancelCommand = null;
            
            base.Dispose();
        }
    }
}
