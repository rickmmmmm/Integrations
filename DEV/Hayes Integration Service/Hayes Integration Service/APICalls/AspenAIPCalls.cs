using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Hayes_Integration_Service.APICalls
{
    class AspenAIPCalls
    {
        public static string PostAPICall(string URL, string Payload)
        {
            HttpResponseMessage Message;
            string Results = "true";
            string StatusCode = "200";

            //var JsonString = JsonConvert.SerializeObject(Payload);
            var Content = new StringContent(Payload, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    Message = client.PostAsync(URL, Content).Result;

                    if (Message.StatusCode.ToString() == "Fee Created")
                    {
                        StatusCode = "201";
                    }

                    if (Message.StatusCode.ToString() == "Failed Validation")
                    {
                        StatusCode = "400";
                    }

                    if (Message.StatusCode.ToString() == "Internal Server Error" || Message.StatusCode.ToString() == "InternalServerError")
                    {
                        StatusCode = "500";
                    }

                    if (Message.StatusCode.ToString() == "API Timeout")
                    {
                        StatusCode = "504";
                    }

                    Results = Message.IsSuccessStatusCode.ToString() + " - StatusCode: (" + StatusCode + ") ErrorMessage: " + Message.ReasonPhrase.ToString() + ")";
                }
                catch (WebException e)
                {
                    Results = e.ToString();
                }

            }

            return Results;
        }

        public static string GETAPICall((string URL, string Payload)
        {


        }
    }
}
