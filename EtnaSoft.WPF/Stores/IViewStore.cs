using System;
using DevExpress.Mvvm;
using EtnaSoft.WPF.ViewModels;

namespace EtnaSoft.WPF.Stores
{
    public interface IViewStore
    {
        event Action ViewChanged;
        EtnaBaseViewModel CurrentViewModel { get; set; }
    }
}