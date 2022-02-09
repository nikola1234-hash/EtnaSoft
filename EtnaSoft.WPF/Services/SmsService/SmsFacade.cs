using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EtnaSoft.Bll.BulkSms.Helpers;
using EtnaSoft.WPF.Services.Configuration;
using EtnaSoft.WPF.ViewModels;

namespace EtnaSoft.WPF.Services.SmsService
{
    public enum ConfigKeys
    {
        SmsUrl,
        SmsUser,
        SmsSecret
    }
    public sealed class SmsFacade : ISmsFacade
    {
        
        private string username { get; }
        private string url { get; }
        private string secret { get; }
        public SmsFacade(string username, string url, string secret)
        {
            CheckArguments(username, url, secret);
            this.username = username;
            this.url = url;
            this.secret = secret;
            SaveValues();
        }
        public SmsFacade()
        {
            
        }
        private static void CheckArguments(string username, string url, string secret)
        {
            if (string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(url) &&
                string.IsNullOrWhiteSpace(secret))
            {
                throw new Exception("Fields cannot be empty");
            }
        }
        private List<string> ProtectData()
        {
            var data = new List<string>
            {
                Protector.Protect(username),
                Protector.Protect(secret)
            };
            return data;
        }
        private void SaveValues()
        {
            ConfigurationFileManager.SetSetting(ConfigKeys.SmsUrl.ToString(), url);
            var protectedData = ProtectData();
            ConfigurationFileManager.SetSetting(ConfigKeys.SmsUser.ToString(), protectedData[0]);
            ConfigurationFileManager.SetSetting(ConfigKeys.SmsSecret.ToString(), protectedData[1]);
        }

        public string GetValue(ConfigKeys key)
        {
            if (key == ConfigKeys.SmsUrl)
            {
                var uri = ConfigurationFileManager.GetSetting(key.ToString());
                return uri;
            }
            var data = ConfigurationFileManager.GetSetting(key.ToString());
            var unprotected = Protector.Unprotect(data);
            return unprotected;
        }

    }
}
