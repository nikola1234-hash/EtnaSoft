using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm;
using EtnaSoft.Dal.Services.Authorization;

namespace EtnaSoft.WPF.ViewModels
{
    public class ChangePasswordDialogViewModel : EtnaBaseViewModel
    {
        public string Username { get; set; }

        public IAuthorization _auth
        {
            get => GetService<IAuthorization>();
        }
        public string Password { get; set; }
        private string _oldPassword;
        public UICommand ChangeCommand { get; set; }
        public string OldPassword
        {
            get { return _oldPassword; }
            set
            {
                _oldPassword = value;
                RaisePropertyChanged(nameof(OldPassword));
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
        public ChangePasswordDialogViewModel(string username, string password)
        {
            Username = username;
            Password = password;
            ChangeCommand = new UICommand()
            {
                Id= MessageBoxResult.OK,
                Caption = "Izmeni",
                IsDefault = true,
                Command = new DelegateCommand(ChangePasswordExecute, CanChangePasswordExecute)
            };
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
            ReleaseResources();
        }
        //TODO: This is an experiment of reflection Disposing properties
        private void ReleaseResources()
        {
            foreach (var property in this.GetType().GetProperties())
            {
                property.SetValue(this, string.Empty);
            }
        }
    }
}
