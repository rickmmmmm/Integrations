using System;
using System.Linq;
using IntegrationConsole.TIPWebIT;

namespace IntegrationConsole
{
    class Program
    {
        static void Main()
        {
            try
            {
                using (WebServiceSoapClient client = new WebServiceSoapClient())
                {
                    client.ExternalBulkUpdate("SAP", "SAP1234!");
                    return;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
