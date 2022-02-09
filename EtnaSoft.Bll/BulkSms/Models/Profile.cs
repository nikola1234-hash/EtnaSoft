using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace EtnaSoft.Bll.BulkSms.Models
{
    public sealed class Profile
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("created")]
        public string Created { get; set; }
        [JsonProperty("credits")]
        public Credits Credits { get; set; }
        [JsonProperty("quota")]
        public Quota Quota { get; set; }
    }
}
