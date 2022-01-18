using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mvvm;
using ErtnaSoft.Bo.Entities;

namespace EtnaSoft.WPF.ViewModels
{
    public sealed class RoomsManagerViewModel : EtnaBaseViewModel
    {

        public ICommand OnLoadCommand { get; }

        public RoomsManagerViewModel()
        {
            OnLoadCommand = new DelegateCommand(OnLoaded);
        }

        private void OnLoaded()
        {
            
        }


        private Room _selectedItem;

        public Room SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        private ObservableCollection<Room> _roomsCollection;

        public ObservableCollection<Room> RoomsCollection
        {
            get { return _roomsCollection; }
            set
            {
                _roomsCollection = value;
                RaisePropertyChanged(nameof(RoomsCollection));
            }
        }


        
    }
}
