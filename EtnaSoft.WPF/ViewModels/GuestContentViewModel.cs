using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Xpf.Bars.Native;
using EtnaSoft.Bll.Models;
using EtnaSoft.Bll.Services;
using EtnaSoft.WPF.Models;

namespace EtnaSoft.WPF.ViewModels
{
    public class GuestContentViewModel : ContentViewModel
    {
        private ObservableCollection<GuestInfo> _guestInfos;

        public ObservableCollection<GuestInfo> GuestInfos
        {
            get { return _guestInfos; }
            set
            {
                _guestInfos = value;
                RaisePropertyChanged(nameof(GuestInfos));
            }
        }
        private string _searchGuest;
        /// <summary>
        /// Guest Search textbox
        /// </summary>
        public string SearchGuest
        {
            get { return _searchGuest; }
            set
            {
                _searchGuest = value;
                RaisePropertyChanged(nameof(SearchGuest));
            }
        }

        void GuestSearch()
        {
            
        }
        private GuestInfo _selectedItem;

        public GuestInfo SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }
        private readonly IGuestService _guestService;
        public ICommand LoadCommand { get; }

        public GuestContentViewModel(IGuestService guestService)
        {
            _guestService = guestService;
            LoadCommand = new DelegateCommand(OnLoad);
        }

        private void OnLoad()
        {
            InitialGuestInfo();
        }

        void SearchGuestByKeyword(string keyword)
        {
            GuestInfos = _guestService.LoadByKeyword(keyword);
        }
        void InitialGuestInfo()
        {
            var guests = _guestService.GetAllGuestData();
            GuestInfos = new ObservableCollection<GuestInfo>(guests);
        }
    }
}
