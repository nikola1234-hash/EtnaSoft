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
    public sealed class CreateUserViewFactory : ICreateUserViewFactory
    {
        private readonly CreateUserViewModel _createUserViewModel;

        public CreateUserViewFactory(CreateUserViewModel createUserViewModel)
        {
            _createUserViewModel = createUserViewModel;
        }

        public ThemedWindow CreateUserWindow()
        {
            var window = new CreateUserWindow
            {
                DataContext = _createUserViewModel
            };
            return window;
        }
        public ThemedWindow CreateView(WindowType windowType)
        {
            switch (windowType)
            {
                case WindowType.CreateUser:
                    return CreateUserWindow();
                default:
                    throw new Exception("Greska pogresan viewType");
            }
        }
    }
}
