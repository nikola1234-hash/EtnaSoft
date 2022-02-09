using System.Configuration;

namespace EtnaSoft.WPF.Services.Configuration
{
    public static class ConfigurationFileManager
    {
        
        public static string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static bool CheckIfThereIsValue(string key)
        {
            bool output = false;
            System.Configuration.Configuration configuration =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var value = configuration.AppSettings.Settings[key].Value;
            if (string.IsNullOrEmpty(value))
            {
                output = true;
            }

            return output;
        }
        public static void SetSetting(string key, string value)
        {
            
            System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
