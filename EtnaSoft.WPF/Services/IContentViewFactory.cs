using EtnaSoft.WPF.ViewModels;

namespace EtnaSoft.WPF.Services
{
    public enum ContentViewType
    {
        UserContent,
        GuestContent,
        CreateGuestContent
    }
    public interface IContentViewFactory
    {
        ContentViewModel CreateContentViewModel(ContentViewType type);
    }
}