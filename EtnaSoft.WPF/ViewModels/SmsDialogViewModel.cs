using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.DataAccess.Native.ExpressionEditor;
using DevExpress.Mvvm;
using EtnaSoft.WPF.Services.Configuration;
using EtnaSoft.WPF.Services.SmsService;
using Microsoft.AspNet.Identity;

namespace EtnaSoft.WPF.ViewModels
{
    public sealed class SmsDialogViewModel : EtnaBaseViewModel
    {
        private string _url;

        public string Url
        {
            get { return _url; }
            set
            {
                _url = value;
                RaisePropertyChanged(nameof(Url));
                CanExecute();
            }
        }
        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                RaisePropertyChanged(nameof(Username));
                CanExecute();
            }
        }
        private string _secret;
        public string Secret
        {
            get { return _secret; }
            set
            {
                _secret = value;
                RaisePropertyChanged(nameof(Secret));
                CanExecute();
            }
        }
        private ISmsFacade Facade { get; set; }

        private UICommand RegisterCommand { get; set; }
        
        private UICommand AbortCommand
        {
            get; set;
        }



        public List<UICommand> Commands { get; set; }


        public SmsDialogViewModel()
        {
            InitializeDialogCommands();
        }

        private void ExecuteAbort(CancelEventArgs args)
        {
            args.Cancel = false;
        }

        private void InitializeDialogCommands()
        {
            RegisterCommand = new UICommand(id: MessageResult.Yes,
                command: new DelegateCommand(ExecuteRegister, CanExecute), isDefault: true, caption: "Registruj", isCancel:false);
            AbortCommand = new UICommand(id: MessageResult.Cancel, caption: "Odustani",
                new DelegateCommand<CancelEventArgs>(ExecuteAbort), isCancel: true, isDefault:false);
            Commands = new List<UICommand>(2);
            Commands.Add(RegisterCommand);
            Commands.Add(AbortCommand);
        }
        private bool FieldsNotEmpty()
        {
            bool output = !string.IsNullOrEmpty(_url) && !string.IsNullOrEmpty(_username) &&
                          !string.IsNullOrEmpty(_secret);
            return output;
        }
        private bool CanExecute()
        {
            return FieldsNotEmpty();
        }

        private void ExecuteRegister()
        {
            try
            {
                Facade = new SmsFacade(Username, Url, Secret);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Obavestenje");
            }

        }
        public override void Dispose()
        {
            base.Dispose();
        }
        
    }
}
