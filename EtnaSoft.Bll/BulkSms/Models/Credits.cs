using Newtonsoft.Json;

namespace EtnaSoft.Bll.BulkSms.Models
{
    public class Credits
    {
        [JsonProperty("balance")]
        public decimal Balance { get; set; }
        [JsonProperty("limit")]
        public decimal Limit { get; set; }
        [JsonProperty("isTransferAllowed")]
        public bool IsTransferAllowed { get; set; }
    }
}