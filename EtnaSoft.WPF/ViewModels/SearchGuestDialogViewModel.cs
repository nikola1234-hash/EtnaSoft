using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Services;

namespace EtnaSoft.WPF.ViewModels
{
    public class SearchGuestDialogViewModel : ViewModelBase
    {
        private readonly IGuestSearchService _guestSearch;
        private string _searchKeyword;

        public string SearchKeyword
        {
            get { return _searchKeyword; }
            set
            {
                _searchKeyword = value;
                RaisePropertyChanged(nameof(SearchKeyword));
            }
        }

        private Guest _selectedGuest;

        public Guest SelectedGuest
        {
            get { return _selectedGuest; }
            set
            {
                _selectedGuest = value;
                RaisePropertyChanged(nameof(SelectedGuest));
            }
        }
        private ObservableCollection<Guest> _guestList;

        public ObservableCollection<Guest> GuestList
        {
            get { return _guestList; }
            set
            {
                _guestList = value;
                RaisePropertyChanged(nameof(GuestList));
            }
        }

        public List<UICommand> DialogCommands { get; private set; }
        protected UICommand CancelUiCommand { get; private set; }
        public ICommand SearchCommand { get; }

        public SearchGuestDialogViewModel(IGuestSearchService guestSearch)
        {
            _guestSearch = guestSearch;
            SearchCommand = new DelegateCommand(SearchCommandExecute);
            DialogCommands = new List<UICommand>();
            CancelUiCommand = new UICommand(
                id: MessageBoxResult.Cancel,
                isCancel: true,
                isDefault: false,
                command: new DelegateCommand<CancelEventArgs>(CancelExecute),
                caption: "Odustani");
            DialogCommands.Add(CancelUiCommand);
            InitializeCollection();
        }

        private void SearchCommandExecute()
        {
            var guests = _guestSearch.GetGuests(SearchKeyword);
            if (GuestList.Count > 0)
            {
                GuestList.Clear();
            }
            //TODO: Write some logic if list is empty
            if (!guests.Any())
            { 

            }
            GuestList = new ObservableCollection<Guest>(guests);
        }

        private void CancelExecute(CancelEventArgs args)
        {
            args.Cancel = true;
        }

        private void InitializeCollection()
        {
            var guests = _guestSearch.GetGuests("");
            GuestList = new ObservableCollection<Guest>(guests);
        }
}
}
