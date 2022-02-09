using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EtnaSoft.Bll.BulkSms.Models;
using EtnaSoft.Bll.Dto;

namespace EtnaSoft.Bll.BulkSms.Services
{
    public sealed class MessageDetailsService : IMessageDetailsService
    {
        private string Username { get; }
        private string Secret { get; }
        private bool RetrieveSentMessages { get; }
        public MessageDetailsService(string username, string secret, bool retrieveSentMessages)
        {
            Username = username;
            Secret = secret;
            RetrieveSentMessages = retrieveSentMessages;
        }


        private async Task<List<ResponseSchema>> GetMessagesAsync()
        {
            GetMessageListService messageListService =
                new GetMessageListService(Username, Secret, RetrieveSentMessages);
            return await messageListService.GetMessages();
        }
        private static MessageDetailsDto MapMessage(ResponseSchema message)
        {
            var mess = new MessageDetailsDto()
            {
                Id = message.Id,
                Body = message.Body,
                CreditCost = message.CreditCost,
                From = message.From,
                NumberOfParts = message.NumberOfParts,
                Status = message.Status.Type,
                To = message.To,
                Type = message.Type
            };
            return mess;
        }
        public async Task<List<MessageDetailsDto>> ReturnMessageDetailsAsync()
        {
            List<MessageDetailsDto> messageDetails = new List<MessageDetailsDto>();
            var messages = await GetMessagesAsync();
            foreach (var message in messages)
            {
                var mapedMessage = MapMessage(message);
                messageDetails.Add(mapedMessage);
            }

            return messageDetails;
        }
    }
}
