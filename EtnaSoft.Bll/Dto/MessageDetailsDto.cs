using EtnaSoft.Bll.BulkSms.Models;

namespace EtnaSoft.Bll.Dto
{
    public class MessageDetailsDto
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
        public int NumberOfParts { get; set; }
        public float CreditCost { get; set; }
        public string Status { get; set; }
    }
}
