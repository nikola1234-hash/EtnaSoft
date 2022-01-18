using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;

namespace EtnaSoft.WPF.ViewModels
{
    public class EtnaBaseViewModel : ViewModelBase, IDisposable
    {
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged(nameof(IsBusy));
            }
        }
        public virtual void Dispose()
        {
        }
    }
}
