using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Model;
using System.Configuration;

namespace Services
{
    public class ElasticMailService : ISender
    {
        public void send(IMessage message)
        {
            NameValueCollection values = new NameValueCollection();
            values.Add("apikey", ConfigurationManager.AppSettings["apikey"]);
            values.Add("from", message.Sender);
            values.Add("fromName", ConfigurationManager.AppSettings["fromName"]);
            values.Add("to", string.Join(",",message.Receivers.ToArray()));
            values.Add("subject", message.Subject);
            values.Add("bodyHtml", message.Body);
            values.Add("isTransactional", "true");

            using (WebClient client = new WebClient())
            {
                try
                {
                    string address = ConfigurationManager.AppSettings["ElasticAPI"];
                    byte[] apiResponse = client.UploadValues(address, values);

                    Console.WriteLine(Encoding.UTF8.GetString(apiResponse));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error sending email. Error Message: " + e.Message);
                }
            }
        }

        public void sendAsync(IMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
