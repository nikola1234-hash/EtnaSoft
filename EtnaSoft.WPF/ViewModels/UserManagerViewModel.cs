using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.LayoutControl;
using System.Windows.Controls;
using EtnaSoft.Bll.Services;
using EtnaSoft.WPF.Infrastructure;

namespace EtnaSoft.WPF.ViewModels
{
    public sealed class UserManagerViewModel : EtnaBaseViewModel
    {
        private ObservableCollection<UserSubViewModel> _userCollection;

        public ObservableCollection<UserSubViewModel> UserCollection
        {
            get { return _userCollection; }
            set
            {
                _userCollection = value;
                RaisePropertyChanged(nameof(UserCollection));
            }
        }

        private readonly IUserManagerService _userManager;
        
        private ICurrentWindowService CurrentWindowService => this.GetService<ICurrentWindowService>();
        public ICommand OnLoadedCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand<object> RightMouseCommand { get; }
        public UserManagerViewModel(IUserManagerService userManager)
        {
            _userManager = userManager;
            
            OnLoadedCommand = new DelegateCommand(OnLoad);
            CloseCommand = new DelegateCommand(OnClosing);
            RightMouseCommand = new DelegateCommand(OnRightMouseDown);
        }

        private void OnRightMouseDown()
        {
            
        }


        private void CloseWindow()
        {
            CurrentWindowService.Close();
        }
        private void OnClosing()
        {
            CloseWindow();
        }

        private void OnLoad()
        {
            UserCollection = new ObservableCollection<UserSubViewModel>();
            var users = _userManager.GetAllUsers().ToList();
            foreach (var user in users)
            {
                UserCollection.Add(new UserSubViewModel(_userManager, user));
            }

            

        }

        public override void Dispose()
        {
            UserCollection.Clear();
            UserCollection = null;
            base.Dispose();
          
        }
        
    }
}
