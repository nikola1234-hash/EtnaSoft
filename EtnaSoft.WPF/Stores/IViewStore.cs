using System;
using DevExpress.Mvvm;

namespace EtnaSoft.WPF.Stores
{
    public interface IViewStore
    {
        event Action ViewChanged;
        ViewModelBase CurrentViewModel { get; set; }
    }
}