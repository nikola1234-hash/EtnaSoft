using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using EtnaSoft.Bll.Services;

namespace EtnaSoft.WPF.ViewModels
{
    public abstract class BaseStayTypeDialogViewModel : EtnaBaseViewModel
    {

       
        
        #region Fields

        public bool IsDirty { get; set; }

        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    IsDirty = true;
                    _title = value;
                    RaisePropertyChanged(nameof(Title));
                    CanExecute();
                }
                

            }
        }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    RaisePropertyChanged(nameof(Price));
                    CanExecute();
                }
                

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
        #endregion

        public abstract bool CanExecute();
        public abstract void Execute();
        public abstract void Abort(CancelEventArgs obj);
    }
}
