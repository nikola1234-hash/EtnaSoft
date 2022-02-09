using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EtnaSoft.Bll.BulkSms.Helpers;
using EtnaSoft.Bll.BulkSms.Models;
using Newtonsoft.Json;

namespace EtnaSoft.Bll.BulkSms.Services
{
    public class GetMessageListService
    {
        private string Username { get; set; }
        private string Secret { get; set; }
        private bool RetrieveSentMessages { get; set; }
        public GetMessageListService(string username, string secret, bool retrieveSentMessages)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(secret))
            {
                throw new Exception("Polja ne mogu biti prazna");
            }
            Username = username;
            Secret = secret;
            RetrieveSentMessages = retrieveSentMessages;
        }
        public async Task<List<ResponseSchema>> GetMessages()
        {
            string sent = "SENT";
            if (RetrieveSentMessages == false)
            {
                sent = "RECEIVED";
            }
            string url = "https://api.bulksms.com/v1/messages?filter=type%3D"+sent;

            var username = Protector.Unprotect(Username);
            var password = Protector.Unprotect(Secret);
            var request = WebRequest.Create(url);
            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                .GetBytes(username + ":" + password));
            request.Headers.Add("Authorization", "Basic " + encoded);
            request.ContentType = "application/json";
            request.Method = "GET";
            
            string responseFromServer = "";
            WebResponse response = await request.GetResponseAsync();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = await reader.ReadToEndAsync();
                // Display the content.
                
            }

            List<ResponseSchema> messagesList = await Task.Factory.StartNew(() =>
                JsonConvert.DeserializeObject<List<ResponseSchema>>(responseFromServer));
            
            // Close the response.
            response.Close();
            return messagesList;
        }
    }
}
