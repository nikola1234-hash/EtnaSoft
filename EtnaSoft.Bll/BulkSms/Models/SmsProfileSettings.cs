using System.Text;

namespace EtnaSoft.Bll.BulkSms.Models
{
    public class SmsProfileSettings
    {
        public string ProfileUrl { get; set; }
        public string User { get; set; }
        public string Secret { get; set; }

        public SmsProfileSettings(string username, string secret)
        {
            User = username;
            Secret = secret;
        }
        public string Encode()
        {
            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                .GetBytes(User + ":" + Secret));
            return encoded;
        }
    }
}
