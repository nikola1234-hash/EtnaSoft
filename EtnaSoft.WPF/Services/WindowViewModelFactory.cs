using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Core;
using EtnaSoft.WPF.ViewModels;
using EtnaSoft.WPF.Window;

namespace EtnaSoft.WPF.Services
{
    public sealed class WindowViewModelFactory : IWindowViewModelFactory
    {
        private readonly RoomsManagerViewModel _roomsManagerViewModel;
        public WindowViewModelFactory(RoomsManagerViewModel roomsManagerViewModel)
        {
            _roomsManagerViewModel = roomsManagerViewModel;
        }

        private ThemedWindow CreateRoomsManagerWindow()
        {
            var window = new RoomsManagerWindow
            {
                DataContext = _roomsManagerViewModel
            };
            return window;
        }

        public ThemedWindow AddViewModel(WindowType windowType)
        {
            switch (windowType)
            {
                case WindowType.RoomsManager:
                    return CreateRoomsManagerWindow();
                default:
                    throw new Exception("No ViewType");
            }
            
        }
    }
}
