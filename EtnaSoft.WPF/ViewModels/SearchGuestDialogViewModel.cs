using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm;

namespace EtnaSoft.WPF.ViewModels
{
    public class SearchGuestDialogViewModel : ViewModelBase
    {
        public List<UICommand> DialogCommands { get; private set; }
        protected UICommand CancelUiCommand { get; private set; }

        public SearchGuestDialogViewModel()
        {
            DialogCommands = new List<UICommand>();
            CancelUiCommand = new UICommand(
                id:MessageBoxResult.Cancel,
                isCancel:true,
                isDefault:false,
                command:new DelegateCommand<CancelEventArgs>(CancelExecute), 
                caption:"Odustani");
            DialogCommands.Add(CancelUiCommand);
        }

        private void CancelExecute(CancelEventArgs args)
        {
            args.Cancel = true;
        }
    }
}
