using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mvvm;
using EtnaSoft.Bll.BulkSms.Models;
using EtnaSoft.Bll.BulkSms.Services;
using EtnaSoft.WPF.Services.Configuration;
using EtnaSoft.WPF.Services.SmsService;

namespace EtnaSoft.WPF.ViewModels.SmsServiceViewModels
{
    public sealed class SmsProfileViewModel :EtnaBaseViewModel
    {
        public Profile Profile
        {
            get;
            set;
        }
        private readonly SmsService _smsService;
        private readonly ISmsFacade _facade = new SmsFacade();
        private string _uri => _facade.GetValue(ConfigKeys.SmsUrl);
        private string _user => _facade.GetValue(ConfigKeys.SmsUser);
        private string _password => _facade.GetValue(ConfigKeys.SmsSecret);
        public ICommand LoadCommand { get; }
        public ICommand CloseCommand { get; }
        private ICurrentWindowService CurrentWindow => GetService<ICurrentWindowService>();
        public SmsProfileViewModel()
        {
            _smsService = new SmsService();
            LoadCommand = new DelegateCommand(OnLoadAsync);
            CloseCommand = new DelegateCommand(OnCloseExecute);
        }

        private void OnCloseExecute()
        {
            CurrentWindow.Close();
        }

        private async void OnLoadAsync()
        {
            Profile = await GetProfileAsync();
        }
        public async Task<Profile> GetProfileAsync()
        {
            var profile =  await _smsService.GetProfileAsync(_user, _password, _uri);
            return profile;
        }
    }
}
