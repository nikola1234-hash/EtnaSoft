using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace EtnaSoft.Bll.BulkSms.Models
{
    public class ResponseSchema
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("from")]
        public string From { get; set; }
        [JsonProperty("to")]
        public string To { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
        [JsonProperty("encoding")]
        public string Encoding { get; set; }
        [JsonProperty("protocolId")]
        public int ProtocolId { get; set; }
        [JsonProperty("messageClass")]
        public int MessageClass { get; set; }
        [JsonProperty("numberOfParts")]
        public int NumberOfParts { get; set; }
        [JsonProperty("creditCost")]
        public float CreditCost { get; set; }
        [JsonProperty("sumbision")]
        public Submission Submission { get; set; }
        [JsonProperty("status")]
        public Status Status { get; set; }
        [JsonProperty("relatedSentMessageId")]
        public string RelatedSentMessageId { get; set; }
        [JsonProperty("userSuppliedId")]
        public string UserSuppliedId { get; set; }
    }

    public class Status
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("subtype")]
        public string Subtype { get; set; }
    }

    public class Submission
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }
}
