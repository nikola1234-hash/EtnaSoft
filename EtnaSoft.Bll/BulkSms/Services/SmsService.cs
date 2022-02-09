using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EtnaSoft.Bll.BulkSms.Models;
using Newtonsoft.Json;

namespace EtnaSoft.Bll.BulkSms.Services
{
    public sealed class SmsService
    {

        public async Task GetProfile()
        {
            string uri = "https://api.bulksms.com/v1/profile";

            var request = WebRequest.Create(uri);
            string username = "E6504B6CBDCA45BFB7C7135BFB8B7CDF-02-2";
            string password = "Kt6kIT7eCQc9_m_5KWWPXONkcTb#*";
            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                .GetBytes(username + ":" + password));
            request.Headers.Add("Authorization", "Basic " + encoded);
            request.ContentType = "application/json";
            request.Method = "GET";
            
            string responseFromServer = "";
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
                // Display the content.
                Console.WriteLine(responseFromServer);
            }

            Profile profile = JsonConvert.DeserializeObject<Profile>(responseFromServer);
            // Close the response.
            response.Close();
            

        }
    }
}
