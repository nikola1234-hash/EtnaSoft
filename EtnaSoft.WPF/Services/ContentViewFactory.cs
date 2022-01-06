using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EtnaSoft.WPF.ViewModels;

namespace EtnaSoft.WPF.Services
{
    public class ContentViewFactory : IContentViewFactory
    {
        private readonly UserContentViewModel _userContentView;
        private readonly GuestContentViewModel _guestContentView;
        private readonly CreateGuestContentViewModel _createGuestContentView;
        public ContentViewFactory(UserContentViewModel userContentView, GuestContentViewModel guestContentView, CreateGuestContentViewModel createGuestContentView)
        {
            _userContentView = userContentView;
            _guestContentView = guestContentView;
            _createGuestContentView = createGuestContentView;
        }

        public ContentViewModel CreateContentViewModel(ContentViewType type)
        {
            switch (type)
            {
                case ContentViewType.UserContent:
                    return _userContentView;
                case ContentViewType.GuestContent:
                    return _guestContentView;
                case ContentViewType.CreateGuestContent:
                    return _createGuestContentView;
                default:
                    throw new Exception("No views");
            }
            
        }
    }
}
