using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.BulkSms.Models;
using Newtonsoft.Json;

namespace EtnaSoft.Bll.BulkSms.Services
{
    public sealed class AvansSmsService
    {
        private string Url { get; }
        private string Username { get; }
        private string Secret { get;}
        public AvansSmsService(string url, string username, string secret)
        {
            Url = url;
            Username = username;
            Secret = secret;
        }
        public async Task<string> SendSmsTask(Guest guest, Reservation reservation, Invoice invoice)
        {
            string url = Url +"messages";
            string message = "Postovani " + guest.FirstName + ", u prilogu su Vam podaci za uplatu avansa." +
                             " Primalac: Usluge smestaja Etna 1," +
                             " Svrha Uplate: Smestaj, Tekuci racun: 205-186658-95," +
                             "Iznos: " + invoice.Avans.ToString("C0") + " Preostali iznos za uplatu: " + invoice.TotalPrice.ToString("C0") + "";
            var telephone = guest.Telephone.Remove(0, 1);
           // var messageRequest = new MessageRequest()
           // {
           //     Body = message,
         //       To = guest.Telephone.ToString()
          //  };



            
            string myData =  "{to: \"381" + telephone + "\", from:\"Vila Etna\", body:\"" + message + "\"}";
            Console.WriteLine(myData);
            
            var request = WebRequest.Create(url);
            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                .GetBytes(Username + ":" + Secret));
            //request.Headers.Add("Authorization", "Basic " + encoded);
            request.Credentials = new NetworkCredential(Username, Secret);
            request.PreAuthenticate = true;
            request.ContentType = "application/json";
            request.Method = "POST";
            var encoding = new UnicodeEncoding();
            var encodedData = encoding.GetBytes(myData);

            // Write the data to the request stream
            var stream = await request.GetRequestStreamAsync();
            await stream.WriteAsync(encodedData, 0, encodedData.Length);
            stream.Close();

            // try ... catch to handle errors nicely
            try
            {
                // make the call to the API
                var response = await request.GetResponseAsync();
                
                // read the response and print it to the console
                var reader = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException());
                return await reader.ReadToEndAsync();
            }
            catch (WebException ex)
            {
                // show the general message
                return "An error occurred:" + ex.Message;

                // print the detail that comes with the error
                var reader = new StreamReader(ex.Response.GetResponseStream());
                return "Error details:" + reader.ReadToEndAsync();
            }
        }
    }
}
