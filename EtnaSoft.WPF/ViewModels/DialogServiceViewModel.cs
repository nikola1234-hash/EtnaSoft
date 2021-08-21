using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using DevExpress.Mvvm;

namespace EtnaSoft.WPF.ViewModels
{
    public class DialogServiceViewModel : ViewModelBase
    {
      public List<UICommand> DialogCommands { get; private set; }
      protected UICommand RegisterUICommand { get; private set; }
      protected UICommand CancelUICommand { get; private set; }
      private bool _allowCloseDialog = false;

      public bool AllowCloseDialog
      {
          get { return _allowCloseDialog; }
          set
          {
              _allowCloseDialog = value;
              SetProperty(ref _allowCloseDialog, value, () => AllowCloseDialog);
          }
      }

      private string _username;

      public string Username
      {
          get { return _username; }
          set
          {
              _username = value;
              SetProperty(ref _username, value, () => Username);
          }
      }

      public DialogServiceViewModel()
      {
          DialogCommands = new List<UICommand>();
          RegisterUICommand = new UICommand(
              id:MessageBoxResult.OK,
              command: new DelegateCommand<CancelEventArgs>(RegisterCommand, CanRegisterExecute),
              isDefault:true,
              isCancel:false,
              caption: "Registruj");
          CancelUICommand = new UICommand
              (
              id: MessageBoxResult.Cancel,
              isCancel:true,
              isDefault:false,
              command:new DelegateCommand<CancelEventArgs>(CancelExecute),
              caption:"Odustani"
              );
          DialogCommands.Add(RegisterUICommand);
          DialogCommands.Add(CancelUICommand);
      }

      private void CancelExecute(CancelEventArgs args)
      {
          if (!_allowCloseDialog)
          {
              args.Cancel = true;
          }
      }

      private bool CanRegisterExecute(CancelEventArgs args)
      {
          return !string.IsNullOrEmpty(Username);
      }

      private void RegisterCommand(CancelEventArgs args)
      {
          if (!_allowCloseDialog)
          {
              args.Cancel = true;
          }
      }
    }
}
