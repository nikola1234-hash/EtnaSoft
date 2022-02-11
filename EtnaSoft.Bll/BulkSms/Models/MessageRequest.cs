using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace EtnaSoft.Bll.BulkSms.Models
{
    public class MessageRequest
    {
        
        [JsonProperty("to")] public string To { get; set; }
        [JsonProperty("body")] public string Body { get; set; }

    }

}
