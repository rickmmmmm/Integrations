using MiddleWay_DTO.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace MiddleWay_Utilities
{
    public class ElasticSmsService // : ISender
    {
        #region Private Variables and Properties
        #endregion Private Variables and Properties

        #region Constructor
        #endregion Constructor

        #region Send Functions

        public void send(MessageModel message, string apiKey, string address)
        {
            //var apiKey = ConfigurationManager.AppSettings["apikey"];
            //string address = ConfigurationManager.AppSettings["SMSAPI"];

            List<NameValueCollection> messageList = new List<NameValueCollection>();

            foreach (var recipient in message.Recipients)
            {
                NameValueCollection values = new NameValueCollection();
                values.Add("apikey", apiKey);
                values.Add("to", recipient);
                values.Add("body", message.Body);
                messageList.Add(values);
            }

            using (WebClient client = new WebClient())
            {
                try
                {
                    foreach (var smsMessage in messageList)
                    {
                        byte[] apiResponse = client.UploadValues(address, smsMessage);

                        Console.WriteLine(Encoding.UTF8.GetString(apiResponse));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void sendAsync(MessageModel message)
        {
            throw new NotImplementedException();
        }

        #endregion Send Functions
    }
}
