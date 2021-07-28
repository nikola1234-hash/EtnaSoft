using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using EtnaSoft.WPF.Navigation;

namespace EtnaSoft.WPF.Services
{
    public interface IEtnaViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}
