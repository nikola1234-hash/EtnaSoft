using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Xpf.Editors;

namespace EtnaSoft.WPF.Stores
{
    public class ViewStore : ViewModelBase, IViewStore
    {
        public event Action ViewChanged;
        private ViewModelBase _currentViewModel
;

        public ViewModelBase CurrentViewModel

        {
            get { return _currentViewModel; }
            set { 
                _currentViewModel = value;
                RaisePropertiesChanged(nameof(CurrentViewModel));
            }
        }
    }
}
