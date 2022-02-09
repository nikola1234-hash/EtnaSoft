using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
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
                CanExecute();
                RaisePropertyChanged(nameof(Url));
            }
        }
        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                CanExecute();
                RaisePropertyChanged(nameof(Username));
            }
        }
        private string _secret;
        public string Secret
        {
            get { return _secret; }
            set
            {
                _secret = value;
                CanExecute();
                RaisePropertyChanged(nameof(Secret));
            }
        }
        private ISmsFacade _facade { get; set; }

        private UICommand RegisterCommand { get; set; }
        
        private UICommand AbortCommand
        {
            get; set;
        }



        public List<UICommand> Commands { get; set; }


        public SmsDialogViewModel()
        {
            RegisterCommand = new UICommand(id: MessageResult.Yes,
                command: new DelegateCommand(ExecuteRegister, CanExecute), isDefault: true, caption: "Registruj", isCancel:false);
            AbortCommand = new UICommand(id: MessageResult.Cancel, caption: "Odustani",
                new DelegateCommand<CancelEventArgs>(ExecuteAbort), isCancel: true, isDefault:false);
            Commands = new List<UICommand>(2);
            Commands.Add(RegisterCommand);
            Commands.Add(AbortCommand);
            
        }

        private void ExecuteAbort(CancelEventArgs args)
        {
            args.Cancel = false;
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
            _facade = new SmsFacade(Username, Url, Secret);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
