﻿using System;
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
    public sealed class SmsService
    {

        public async Task<Profile> GetProfileAsync(string username, string password, string uri)
        {
            string url = uri +"profile";

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

            Profile profile = JsonConvert.DeserializeObject<Profile>(responseFromServer);
            // Close the response.
            response.Close();
            return profile;
        }
    }
}
