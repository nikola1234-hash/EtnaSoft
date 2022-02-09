using Newtonsoft.Json;

namespace EtnaSoft.Bll.BulkSms.Models
{
    public class Quota
    {
        [JsonProperty("size")]
        public int Size
        {
            get; set;
        }
        [JsonProperty("remaining")]
        public int Remaining { get; set; }
    }
}