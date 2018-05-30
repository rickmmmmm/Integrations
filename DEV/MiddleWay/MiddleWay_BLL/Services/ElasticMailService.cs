using MiddleWay_DTO.Models;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace Services
{
    public class ElasticMailService : ISender
    {
        public void send(MessageModel message)
        {
            var apiKey = ConfigurationManager.AppSettings["apikey"];
            var fromName = ConfigurationManager.AppSettings["fromName"];
            string address = ConfigurationManager.AppSettings["ElasticAPI"];

            var toRecipients = string.Join(",", message.Recipients.ToArray());

            if (message is EmailMessageModel)
            {
                var emailMessage = (EmailMessageModel)message;
                NameValueCollection values = new NameValueCollection();
                values.Add("apikey", apiKey);
                values.Add("from", emailMessage.Sender);
                values.Add("fromName", fromName);
                values.Add("to", toRecipients);
                values.Add("subject", emailMessage.Subject);
                values.Add("bodyHtml", emailMessage.Body);
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
        }

        public void sendAsync(MessageModel message)
        {
            throw new NotImplementedException();
        }
    }
}
