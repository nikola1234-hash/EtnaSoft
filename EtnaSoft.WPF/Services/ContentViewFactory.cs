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

        public ContentViewFactory(UserContentViewModel userContentView)
        {
            _userContentView = userContentView;
        }

        public ContentViewModel CreateContentViewModel(ContentViewType type)
        {
            switch (type)
            {
                case ContentViewType.UserContent:
                    return _userContentView;
                
                default:
                    throw new Exception("No views");
            }
            
        }
    }
}
