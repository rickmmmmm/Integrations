using MiddleWay_DTO.Models;
using MiddleWay_DTO.ServiceInterfaces;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace MiddleWay_Utilities
{
    public class ElasticMailService : IElasticMailService
    {
        public void send(EmailMessageModel message, string apiKey, string fromName, string address)
        {
            //var apiKey = ConfigurationManager.AppSettings["apikey"];
            //var fromName = ConfigurationManager.AppSettings["fromName"];
            //string address = ConfigurationManager.AppSettings["ElasticAPI"];

            var toRecipients = string.Join(",", message.Recipients.ToArray());

            //var emailMessage = (EmailMessageModel)message;
            NameValueCollection values = new NameValueCollection();
            values.Add("apikey", apiKey);
            values.Add("from", message.Sender);
            values.Add("fromName", fromName);
            values.Add("to", toRecipients);
            values.Add("subject", message.Subject);
            values.Add("bodyHtml", message.Body);
            values.Add("isTransactional", "true");

            using (WebClient client = new WebClient())
            {
                try
                {
                    byte[] apiResponse = client.UploadValues(address, values);

                    Console.WriteLine(Encoding.UTF8.GetString(apiResponse));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error sending email. Error Message: " + e.Message);
                }
            }

        }

        public void sendAsync(EmailMessageModel message, string apiKey, string fromName, string address)
        {
            throw new NotImplementedException();
        }
    }
}
