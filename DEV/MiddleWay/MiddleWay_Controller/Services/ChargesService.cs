using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.Services
{
    public class ChargesService
    {

        public void createExportFile(List<ChargeExportFile> results, string fileName)
        {
            using (StreamWriter writer = File.AppendText(fileName))
            {
                var csv = new CsvWriter(writer);
                csv.Configuration.Delimiter = ConfigurationManager.AppSettings["delimiter"];
                //csv.Configuration.IgnoreQuotes = true;
                csv.Configuration.HasHeaderRecord = false;
                //csv.Configuration.Quote = ConfigurationManager.AppSettings["textQualifier"].ToCharArray()[0];
                //csv.Configuration.QuoteAllFields = true;

                csv.WriteRecords(results);
            }
        }

    }
}
