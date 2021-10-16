using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EtnaSoft.WPF.ViewModels;

namespace EtnaSoft.WPF.Stores
{
    public class ContentViewStore : ContentViewModel, IContentViewStore
    {
        public event Action ContentViewChanged;

        private ContentViewModel _currentContentView;

        public ContentViewModel CurrentContentView
        {
            get { return _currentContentView; }
            set
            {
                _currentContentView?.Dispose();
                _currentContentView = value;
                ContentViewChanged.Invoke();
            }
        }
    }
}
