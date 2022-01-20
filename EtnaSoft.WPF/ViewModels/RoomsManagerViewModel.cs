using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mvvm;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Services;
using EtnaSoft.WPF.Events;
using Prism.Events;

namespace EtnaSoft.WPF.ViewModels
{
    public sealed class RoomsManagerViewModel : EtnaBaseViewModel
    {
        private readonly IRoomsManagerService _roomsManagerService;
        private ICurrentWindowService CurrentWindowService => GetService<ICurrentWindowService>();
        private readonly IEventAggregator _eventAggregator;
        public ICommand OnLoadCommand { get; }
        public ICommand CloseCommand { get; }
        private ObservableCollection<SubRoomViewModel> _roomsCollection = new ObservableCollection<SubRoomViewModel>();

        public ObservableCollection<SubRoomViewModel> RoomsCollection
        {
            get { return _roomsCollection; }
            set
            {
                _roomsCollection = value;
                RaisePropertyChanged(nameof(RoomsCollection));
            }
        }

        public RoomsManagerViewModel(IRoomsManagerService roomsManagerService, IEventAggregator eventAggregator)
        {
            _roomsManagerService = roomsManagerService;
            _eventAggregator = eventAggregator;
            OnLoadCommand = new DelegateCommand(OnLoaded);
            CloseCommand = new DelegateCommand(OnCloseCommand);
        }

        private void CloseWindow()
        {
            _eventAggregator.GetEvent<WindowManagerOpenEvent>().Publish();
            CurrentWindowService?.Close();
        }
        private void OnCloseCommand()
        {
            CloseWindow();
        }

        void PopulateView()
        {

            var rooms = _roomsManagerService.GetAllRooms();
            if (RoomsCollection != null)
            {
                RoomsCollection?.Clear();
                
                foreach (var room in rooms)
                {
                    RoomsCollection.Add(new SubRoomViewModel(room));
                }
            }
            else
            {
                RoomsCollection = new ObservableCollection<SubRoomViewModel>();
                foreach (var room in rooms)
                {
                    RoomsCollection.Add(new SubRoomViewModel(room));
                }
            }
        }

        private void OnLoaded()
        {
            PopulateView();
        }

        public override void Dispose()
        {
            RoomsCollection = null;
            base.Dispose();
        }
    }
}
