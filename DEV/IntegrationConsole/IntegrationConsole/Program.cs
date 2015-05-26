﻿using System;
using System.Linq;
using IntegrationConsole.TIPWebIT;

namespace IntegrationConsole
{
    class Program
    {
        static int Main(string[] args)
        {
            using (WebServiceSoapClient client = new WebServiceSoapClient())
            {
                client.ExternalBulkUpdate("SAP", "SAP1234!");
                return 0;
            }
        }
    }
}
