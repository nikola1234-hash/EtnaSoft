using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Xpf.Editors;
using EtnaSoft.WPF.ViewModels;

namespace EtnaSoft.WPF.Stores
{
    public class ViewStore : EtnaBaseViewModel, IViewStore
    {
        public event Action ViewChanged;
        private EtnaBaseViewModel _currentViewModel
;

        public EtnaBaseViewModel CurrentViewModel

        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel?.Dispose();
                _currentViewModel = value;
                ViewChanged.Invoke();
            }
        }

    }
}
