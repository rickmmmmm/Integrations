using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.Services
{
    public class ChargePaymentsService
    {

        public List<PaymentImportFile> serializeChargePaymentsFile(string fileName)
        {
            using (StreamReader reader = File.OpenText(fileName))
            {
                var payload = new List<PaymentImportFile>();
                var csv = new CsvReader(reader);

                csv.Configuration.Delimiter = ConfigurationManager.AppSettings["delimiter"];
                csv.Configuration.HasHeaderRecord = false;
                //csv.Configuration.Quote = ConfigurationManager.AppSettings["textQualifier"].ToCharArray()[0];
                csv.Configuration.IgnoreQuotes = true;

                while (csv.Read())
                {

                    PaymentImportFile newLine = new PaymentImportFile
                    {
                        FineId = csv.GetField<int>(Convert.ToInt32(ConfigurationManager.AppSettings["FineId"])),
                        Amount = csv.GetField<string>(Convert.ToInt32(ConfigurationManager.AppSettings["PaymentAmount"])),
                        Type = csv.GetField<string>(Convert.ToInt32(ConfigurationManager.AppSettings["PaymentType"])),
                        Date = csv.GetField<DateTime>(Convert.ToInt32(ConfigurationManager.AppSettings["PaymentDate"]))
                    };

                    payload.Add(newLine);
                }

                return payload;
            }
        }


    }
}
