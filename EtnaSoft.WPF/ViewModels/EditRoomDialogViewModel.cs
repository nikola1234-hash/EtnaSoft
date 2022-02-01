using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Services;

namespace EtnaSoft.WPF.ViewModels
{
    public sealed class EditRoomDialogViewModel : BaseRoomEditViewModel
    {
        private UICommand ExecuteCommand { get; set; }
        private UICommand AbortCommand { get; set; }
        public List<UICommand> Commands { get; set; }
        private readonly IRoomsManagerService _roomsManagerService;
        public EditRoomDialogViewModel(Room room, IRoomsManagerService roomsManagerService) : base(room)
        {
            _roomsManagerService = roomsManagerService;
            ExecuteCommand = new UICommand(id: MessageResult.OK, "Izmeni", new DelegateCommand(Execute, CanExecute), isCancel: false,
                isDefault: true);
            AbortCommand = new UICommand(id: MessageResult.Cancel, "Izlaz",
                command: new DelegateCommand<CancelEventArgs>(Cancel), isCancel: true, isDefault: false);
            Commands = new List<UICommand>();
            Commands.Add(ExecuteCommand);
            Commands.Add(AbortCommand);
        }

        private void Cancel(CancelEventArgs obj)
        {
            obj.Cancel = false;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute()
        {
            try
            {
                _roomsManagerService.UpdateRoom(Room.Id, Room);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public override void Abort()
        {
            throw new NotImplementedException();
        }
    }
}
