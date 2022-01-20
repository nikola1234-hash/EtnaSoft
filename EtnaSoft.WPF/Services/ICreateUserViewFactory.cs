using DevExpress.Xpf.Core;

namespace EtnaSoft.WPF.Services
{
    public interface ICreateUserViewFactory
    {
        
        ThemedWindow CreateView(WindowType windowType);
        ThemedWindow CreateUserWindow();
    }
}