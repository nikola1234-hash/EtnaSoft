using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mvvm;
using EtnaSoft.WPF.Commands;
using EtnaSoft.WPF.Services;
using EtnaSoft.WPF.Stores;
using EtnaSoft.WPF.Window;

namespace EtnaSoft.WPF.ViewModels
{
    public class HomeViewModel : EtnaBaseViewModel
    {
        public ContentViewModel ContentViewModel => _contentStore.CurrentContentView;
        public ICommand NavigateContentCommand { get; }
        public ICommand UserSettingCommand { get; }
        public ICommand CreateUserCommand { get; }
        public ICommand<WindowType> OpenRoomManagerCommand { get; }
        private readonly IContentViewStore _contentStore;
        private readonly IContentViewFactory _contentFactory;
        private readonly IWindowViewModelFactory _windowFactory;
        public HomeViewModel(IContentViewStore contentStore, IContentViewFactory contentFactory, IWindowViewModelFactory windowFactory)
        {
            _contentStore = contentStore;
            _contentFactory = contentFactory;
            _windowFactory = windowFactory;
            _contentStore.ContentViewChanged += OnContentViewChanged;
            UserSettingCommand = new DelegateCommand(OpenUsersDialogWindow);
            CreateUserCommand = new DelegateCommand(CreateUser);
            OpenRoomManagerCommand = new DelegateCommand<WindowType>(OpenRoomManager);

            NavigateContentCommand = new NavigateContentCommand(_contentFactory, _contentStore);
        }

        private void OpenRoomManager(WindowType windowType)
        {
            var window = _windowFactory.AddViewModel(windowType);
            window.Show();
        }

        private void OnContentViewChanged()
        {
            RaisePropertiesChanged(nameof(ContentViewModel));
        }

        private void CreateUser()
        {
            throw new NotImplementedException();
        }

        private void OpenUsersDialogWindow()
        {
            //Open dialog window
            //for Creating, deleting and editing users

            throw new NotImplementedException();
        }


        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
