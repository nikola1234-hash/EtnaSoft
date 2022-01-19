using DevExpress.Xpf.Core;
using EtnaSoft.WPF.ViewModels;

namespace EtnaSoft.WPF.Services
{
    public enum WindowType
    {
        RoomsManager,
        StayTypeManager,
        UserManager
    }
    public interface IWindowViewModelFactory
    {
        ThemedWindow AddViewModel(WindowType windowType);
    }
}