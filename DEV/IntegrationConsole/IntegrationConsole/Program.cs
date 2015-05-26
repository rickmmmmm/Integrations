using System;
using System.Linq;

namespace IntegrationConsole
{
    class Program
    {
        static int Main(string[] args)
        {
            using (TIPWebIT.WebServiceSoapClient client = new TIPWebIT.WebServiceSoapClient())
            {
                client.ExternalBulkUpdate("SAP", "SAP1234!");
                return 0;
            }
        }
    }
}
