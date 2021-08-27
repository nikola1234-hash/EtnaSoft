using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using EtnaSoft.WPF.Navigation;
using EtnaSoft.WPF.ViewModels;

namespace EtnaSoft.WPF.Services
{
    public interface IEtnaViewModelFactory
    {
        EtnaBaseViewModel CreateViewModel(ViewType viewType);
    }
}
