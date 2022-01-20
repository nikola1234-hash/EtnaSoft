using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public List<Room> Rooms
        {
            get
            {
                return RoomList();
            }
            set
            {
                Rooms = value;
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

        private List<Room> RoomList()
        {
            return _roomsManagerService.GetAllRooms();
        }
        void PopulateView()
        {

            if (RoomsCollection != null)
            {
                RoomsCollection?.Clear();
                
                foreach (var room in Rooms)
                {
                    RoomsCollection.Add(new SubRoomViewModel(room));
                }
            }
            else
            {
                RoomsCollection = new ObservableCollection<SubRoomViewModel>();
                foreach (var room in Rooms)
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
            Rooms = null;
            base.Dispose();
        }
    }
}
