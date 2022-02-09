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
        private readonly CreateUserViewModel _createUserViewModel;
        private readonly SmsManagerViewModel _smsManagerViewModel;
        public WindowViewModelFactory(RoomsManagerViewModel roomsManagerViewModel, StayTypesManagerViewModel stayTypesManagerViewModel, UserManagerViewModel userManagerViewModel, CreateUserViewModel createUserViewModel, SmsManagerViewModel smsManagerViewModel)
        {
            _roomsManagerViewModel = roomsManagerViewModel;
            _stayTypesManagerViewModel = stayTypesManagerViewModel;
            _userManagerViewModel = userManagerViewModel;
            _createUserViewModel = createUserViewModel;
            _smsManagerViewModel = smsManagerViewModel;
        }

        private ThemedWindow CreateRoomsManagerWindow()
        {
            var window = new RoomsManagerWindow
            {
                DataContext = _roomsManagerViewModel
            };
            return window;
        }

        private ThemedWindow CreateSmsManagerWindow()
        {
            var window = new SmsManagerWindow
            {
                DataContext = _smsManagerViewModel
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

        private ThemedWindow CreateUserCreateWindow()
        {
            var window = new CreateUserWindow()
            {
                DataContext = _createUserViewModel
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
                case WindowType.CreateUser:
                    return CreateUserCreateWindow();
                case WindowType.SmsManager:
                    return CreateSmsManagerWindow();
                default:
                    throw new Exception("No ViewType");
            }
            
        }
    }
}
