using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using DevExpress.Mvvm;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Dto;
using EtnaSoft.Bll.Services;

namespace EtnaSoft.WPF.ViewModels
{
    public sealed class CreateStayTypeDialogViewModel : EtnaBaseViewModel
    {
        private readonly ISpecialTypeService _specialTypeService;
        public List<UICommand> UICommands { get; set; }

        protected UICommand RegisterCommand
        {
            get; set; 

        }
        protected UICommand CancelCommand { get; set; }

        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                RaisePropertyChanged(nameof(Price));
            }
        }

        private bool _isSpecialType;

        public bool IsSpecialType
        {
            get { return _isSpecialType; }
            set
            {
                
                _isSpecialType = value;
                if (_isSpecialType)
                {
                    _cenaLabel = "Cena promocije:";
                    RaisePropertyChanged(nameof(CenaLabel));
                }
                RaisePropertyChanged(nameof(IsSpecialType));
            }
        }

        private decimal _childPrice;

        public decimal ChildPrice
        {
            get { return _childPrice; }
            set
            {
                _childPrice = value;
                RaisePropertyChanged(nameof(ChildPrice));
            }
        }

        private string _cenaLabel = "Jedinicna cena:";

        public string CenaLabel
        {
            get { return _cenaLabel; }
            set
            {
                _cenaLabel = value;

                RaisePropertiesChanged(nameof(CenaLabel));
            }
        }
        private decimal _promoPrice;


        private int _stayDays;

        public int StayDays
        {
            get { return _stayDays; }
            set
            {
                _stayDays = value;
                RaisePropertyChanged(nameof(StayDays));
            }
        }
        private List<Promotion> _listaPromocija;

        public List<Promotion> ListaPromocija
        {
            get { return _listaPromocija; }
            set
            {
                _listaPromocija = value;
                RaisePropertyChanged(nameof(ListaPromocija));
            }
        }

        private Promotion _selectedPromotion;

        public Promotion SelectedPromotion
        {
            get { return _selectedPromotion; }
            set
            {
                _selectedPromotion = value;
                RaisePropertyChanged(nameof(SelectedPromotion));
            }
        }
        public ICommand OnLoadCommand { get; }

        public CreateStayTypeDialogViewModel(ISpecialTypeService specialTypeService)
        {
            _specialTypeService = specialTypeService;
            
            OnLoadCommand = new DelegateCommand(OnLoad);
            RegisterCommand = new UICommand()
            {
                Id= MessageResult.OK,
                IsDefault = true,
                Command = new DelegateCommand(RegisterStayTypeExecute, CanExecute),
                Caption = "Registruj",
                IsCancel = false,

            };
            CancelCommand = new UICommand()
            {
                Id = MessageResult.Cancel,
                IsCancel = true,
                IsDefault = false,
                Caption = "Nazad",
                Command = new DelegateCommand<CancelEventArgs>(CancelDialog)
            };
            UICommands = new List<UICommand>();
            UICommands.Add(RegisterCommand);
            UICommands.Add(CancelCommand);
        }

       


        private void OnLoad()
        {
        
        }

        private void CancelDialog(CancelEventArgs obj)
        {
            obj.Cancel = false;
        }

        private void RegisterStayTypeExecute()
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

        private bool CanExecute()
        {
            //Logic for can Execute for now is true
            return true;
        }
    }
}
