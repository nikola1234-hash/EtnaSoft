using System;
using System.Collections.Generic;
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
    public sealed class CreateRoomViewModel : BaseRoomEditViewModel
    {
        private UICommand ExecuteCommand { get; set; }
        private UICommand AbortCommand { get; set; }
        public List<UICommand> Commands { get; set; }
        private readonly IRoomsManagerService _roomsManagerService;
        public CreateRoomViewModel(Room room, IRoomsManagerService roomsManagerService) : base(room)
        {
            _roomsManagerService = roomsManagerService;
            ExecuteCommand = new UICommand(id: MessageResult.OK, "Upisi", new DelegateCommand(Execute, CanExecute), isCancel: false,
                isDefault: true);
            AbortCommand = new UICommand(id: MessageResult.Cancel, "Izlaz",
                command: new DelegateCommand<CancelEventArgs>(Cancel), isCancel: true, isDefault: false);
            Commands = new List<UICommand>();
            Commands.Add(ExecuteCommand);
            Commands.Add(AbortCommand);
        }

        public override bool CanExecute()
        {
            //bool output = false;
            //var roomNumber = _roomsManagerService.GetAllRooms().FirstOrDefault(s => s.RoomNumber == Room.RoomNumber);
            //output = roomNumber is null && !string.IsNullOrEmpty(Room.RoomNumber);
            //return output;
            return true;
        }

        public override void Execute()
        {
            _roomsManagerService.CreateNewRoom(Room);
        }

        public override void Abort()
        {
        }

        void Cancel(CancelEventArgs args)
        {
            args.Cancel = false;
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}

