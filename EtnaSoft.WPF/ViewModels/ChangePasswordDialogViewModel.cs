using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using EtnaSoft.Dal.Services.Authorization;

namespace EtnaSoft.WPF.ViewModels
{
    public class ChangePasswordDialogViewModel : EtnaBaseViewModel
    {
        public string Username { get; set; }

        private readonly IAuthorization _auth;
        public string Password { get; set; }
        private string _oldPassword;
        public UICommand ChangeCommand { get; set; }
        public List<UICommand> Commands { get; set; }
        public string OldPassword
        {
            get { return _oldPassword; }
            set
            {
                _oldPassword = value;
                RaisePropertyChanged(nameof(OldPassword));
                RaisePropertyChanged(nameof(OldPasswordMatch));
            }
        }


        private bool _oldPasswordMatch;

        public bool OldPasswordMatch
        {
            get => _oldPasswordMatch;
            set
            {
                
                if (Password != OldPassword)
                {
                    ErrorMessage = "Ne poklapaju se stare sifre";
                }
                else
                {
                    _oldPasswordMatch = value;
                    RaisePropertiesChanged(nameof(OldPasswordMatch));
                    
                }
               
            }
        }

        private string _newPassword;

        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                RaisePropertyChanged(nameof(NewPassword));
                RaisePropertyChanged(nameof(NewPasswordMatch));
            }
        }

        private string _repeatNewPassword;

        public string RepeatNewPassword
        {
            get { return _repeatNewPassword; }
            set
            {
                _repeatNewPassword = value;
                RaisePropertyChanged(nameof(RepeatNewPassword));
                RaisePropertyChanged(nameof(NewPasswordMatch));
            }
        }

        private bool _newPasswordMatch;

        public bool NewPasswordMatch
        {
            get { return _newPasswordMatch; }
            set
            {
                if (NewPassword != RepeatNewPassword)
                {
                    ErrorMessage = "Nove sifre se ne poklapaju";
                }
                else
                {
                    _newPasswordMatch = value;
                    RaisePropertyChanged(nameof(NewPasswordMatch));

                }
             
            }
        }
        public MessageViewModel ErrorMessageViewModel { get; set; }

        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }
        public ChangePasswordDialogViewModel(string username, string password, IAuthorization auth)
        {
            Username = username;
            Password = password;
            _auth = auth;
            Commands = new List<UICommand>();
            ChangeCommand = new UICommand
            {
                Id = MessageBoxResult.OK,
                Caption = "Izmeni",
                IsDefault = true,
                Command = new DelegateCommand(ChangePasswordExecute),
                Placement = Dock.Bottom,
                Alignment = DialogButtonAlignment.Center
            };
            Commands.Add(ChangeCommand);
        }

        private bool CanChangePasswordExecute()
        {
            return OldPasswordMatch && NewPasswordMatch;
        }

        private void ChangePasswordExecute()
        {
            ChangePasswordStatus status;
            try
            {
                status = _auth.ChangePassword(Username, NewPassword, Password);
            }
            catch
            {
                throw;
            }

            if (status == ChangePasswordStatus.Failed)
            {
                MessageBox.Show("Greska u promeni sifre");
            }

        }
        public override void Dispose()
        {
            base.Dispose();
         
        }
    }
}
