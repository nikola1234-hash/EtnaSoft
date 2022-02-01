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
        private IDialogService DialogService => GetService<IDialogService>();
        private CreateRoomViewModel createRoomViewModel;
        private readonly IEventAggregator _eventAggregator;
        public ICommand OnLoadCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand CreateRoomCommand { get; }
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
        
        }


        public RoomsManagerViewModel(IRoomsManagerService roomsManagerService, IEventAggregator eventAggregator)
        {
            _roomsManagerService = roomsManagerService;
            _eventAggregator = eventAggregator;
            OnLoadCommand = new DelegateCommand(OnLoaded);
            CloseCommand = new DelegateCommand(OnCloseCommand);
            CreateRoomCommand = new DelegateCommand(OpenDialog);
            _eventAggregator.GetEvent<RoomDataChangeEvent>().Subscribe(PopulateView);
            _eventAggregator.GetEvent<RoomEditOpenDialogEvent>().Subscribe(OpenEditDialog);
        }

        private void OpenEditDialog(object obj)
        {
            if (obj is EditRoomDialogViewModel viewModel)
            {
                DialogService.ShowDialog(viewModel.Commands, "Izmeni", viewModel);
            }
            PopulateView();
        }

        private void OpenDialog()
        {
            
            createRoomViewModel = new CreateRoomViewModel(new Room(), _roomsManagerService);
            var result = DialogService.ShowDialog(createRoomViewModel.Commands, "Kreiraj zapis", createRoomViewModel);
            if ((MessageResult)result.Id == MessageResult.OK)
            {
                PopulateView();
            }

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
                    PopulateRoomsCollection(room);
                }
            }
            else
            {
                RoomsCollection = new ObservableCollection<SubRoomViewModel>();
                foreach (var room in Rooms)
                {
                    PopulateRoomsCollection(room);
                }
            }
        }

        void PopulateRoomsCollection(Room room)
        {
            var srvm = new SubRoomViewModel(room, _roomsManagerService, _eventAggregator);
            RoomsCollection.Add(srvm);
        }

        private void OnLoaded()
        {
            PopulateView();
        }

        public override void Dispose()
        {
            RoomsCollection = null;
            _eventAggregator.GetEvent<RoomDataChangeEvent>().Unsubscribe(PopulateView);
            _eventAggregator.GetEvent<RoomEditOpenDialogEvent>().Unsubscribe(OpenEditDialog);
            base.Dispose();
        }
    }
}
