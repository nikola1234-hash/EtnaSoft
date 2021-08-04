using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using EtnaSoft.WPF.ViewModels;

namespace EtnaSoft.WPF.Stores
{
    public class Renavigate : IRenavigate
    {
        private readonly HomeViewModel _homeView;
        public Renavigate(HomeViewModel homeView)
        {
            _homeView = homeView;
        }

        public ViewModelBase Navigate()
        {
            return _homeView;
        }
    }
}
