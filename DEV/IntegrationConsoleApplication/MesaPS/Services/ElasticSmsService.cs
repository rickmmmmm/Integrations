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
    public class ElasticSmsService : ISender
    {
        public void send(IMessage message)
        {
            NameValueCollection values = new NameValueCollection();
            values.Add("apikey", ConfigurationManager.AppSettings["apikey"]);
            values.Add("to", message.Sender);
            values.Add("body", message.Body);

            using (WebClient client = new WebClient())
            {
                try
                {
                    string address = ConfigurationManager.AppSettings["SMSAPI"];
                    byte[] apiResponse = client.UploadValues(address, values);

                    Console.WriteLine(Encoding.UTF8.GetString(apiResponse));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void sendAsync(IMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
