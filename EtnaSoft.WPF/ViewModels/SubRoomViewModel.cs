using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Services;
using EtnaSoft.Dal.Exceptions;
using EtnaSoft.WPF.Events;
using Prism.Events;

namespace EtnaSoft.WPF.ViewModels
{
    public sealed class SubRoomViewModel :EtnaBaseViewModel
    {
        public Room Room { get; set; }
        public string RoomNumber { get; set; }

        public string IsActive
        {
            get { return @"C:\InDevelopment\EtnaSoft\EtnaSoft.WPF\Icons\hotelbed.svg"; }
        }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        private readonly IRoomsManagerService _roomsManagerService;
        private readonly IEventAggregator _eventAggregator;


        public SubRoomViewModel(Room room, IRoomsManagerService roomsManagerService, IEventAggregator eventAggregator)
        {
            Room = room;
            _roomsManagerService = roomsManagerService;
            _eventAggregator = eventAggregator;
            RoomNumber = room.RoomNumber;
            EditCommand = new DelegateCommand(Update);
            DeleteCommand = new DelegateCommand(DeleteRoom);
        }

        private void Update()
        {
            var viewModel = new EditRoomDialogViewModel(Room, _roomsManagerService);
            OnUpdateContextButtonClick(viewModel);
            OnRoomsDataChange();
        }

        void OnUpdateContextButtonClick(object viewModel)
        {
            _eventAggregator.GetEvent<RoomEditOpenDialogEvent>().Publish(viewModel);
        }
        void OnRoomsDataChange()
        {
            _eventAggregator.GetEvent<RoomDataChangeEvent>().Publish();
        }
        private void DeleteRoom()
        {
            try
            {
                _roomsManagerService.DeleteRoom(Room.Id);
            }
            catch (RoomHasRecordsException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            OnRoomsDataChange();
        }

    }
}
