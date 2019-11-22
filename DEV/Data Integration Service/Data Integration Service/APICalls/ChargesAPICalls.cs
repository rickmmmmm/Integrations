using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;


namespace Data_Integration_Service.APICalls
{
    public class APICharges
    {
        public static string PostAPICall(string URL, string Payload)
        {
            string Results = "Failure";

            var JsonString = JsonConvert.SerializeObject(Payload);
            var Content = new StringContent(JsonString, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Results = client.PostAsync(URL, Content).Result.ToString();
            }

            return Results; 
        }
    }
}
