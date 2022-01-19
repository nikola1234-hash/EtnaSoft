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
        private readonly StayTypesManagerViewModel _stayTypesManagerViewModel;
        private readonly UserManagerViewModel _userManagerViewModel;
        
        public WindowViewModelFactory(RoomsManagerViewModel roomsManagerViewModel, StayTypesManagerViewModel stayTypesManagerViewModel, UserManagerViewModel userManagerViewModel)
        {
            _roomsManagerViewModel = roomsManagerViewModel;
            _stayTypesManagerViewModel = stayTypesManagerViewModel;
            _userManagerViewModel = userManagerViewModel;
            
        }

        private ThemedWindow CreateRoomsManagerWindow()
        {
            var window = new RoomsManagerWindow
            {
                DataContext = _roomsManagerViewModel
            };
            return window;
        }

        private ThemedWindow CreateStayTypesManagerWindow()
        {
            var window = new StayTypesManagerWindow
            {
                DataContext = _stayTypesManagerViewModel
            };
            return window;
        }

        private ThemedWindow CreateUserManagerWindow()
        {
            var window = new UserManagerWindow()
            {
                DataContext = _userManagerViewModel
            };
            return window;
        }

        public ThemedWindow AddViewModel(WindowType windowType)
        {
            switch (windowType)
            {
                case WindowType.RoomsManager:
                    return CreateRoomsManagerWindow();
                case WindowType.StayTypeManager:
                    return CreateStayTypesManagerWindow();
                case WindowType.UserManager:
                    return CreateUserManagerWindow();
                default:
                    throw new Exception("No ViewType");
            }
            
        }
    }
}
