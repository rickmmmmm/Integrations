using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Model;
using System.Configuration;

namespace SystemTasks
{
    public class FileTasks
    {
        //Class implements all functionality for file tasks on csv files.
        public enum ImportType { PurchaseOrder, MobileDeviceManagement }

        public bool checkFile(string fileName)
        {
            try
            {
                var file = File.Open(fileName, FileMode.Open);
                file.Close();
                file.Dispose();
                return true;
            }
            catch
            {
                return false;
            }

        }

        private List<PurchaseOrderFile> serializePurchaseOrderFile(string fileName)
        {

            using (StreamReader reader = File.OpenText(fileName))
            {
                var csv = new CsvReader(reader);

                csv.Configuration.RegisterClassMap<PurchaseOrderClassMap>();
                csv.Configuration.Delimiter = ConfigurationManager.AppSettings["delimiter"];

                var payload = csv.GetRecords<PurchaseOrderFile>().ToList();

                return payload;
            }

            
        }

        public void createRejectFile(string fileName, List<RejectedRecord> rejects)
        {
            using (StreamWriter writer = File.AppendText(fileName))
            {
                var csv = new CsvWriter(writer);
                csv.Configuration.Delimiter = ConfigurationManager.AppSettings["delimiter"];
                csv.Configuration.Quote = ConfigurationManager.AppSettings["textQualifier"].ToCharArray()[0];
                csv.Configuration.QuoteAllFields = true;

                csv.WriteRecords(rejects);
            }
        }

        public dynamic convertCsvFileToObject(string fileName, ImportType type)
        {
            switch (type)
            {
                case ImportType.PurchaseOrder:
                    return serializePurchaseOrderFile(fileName);
                case ImportType.MobileDeviceManagement:
                    return false;
            }

            return true;
        }



        //TODO:
        //Convert file to object
        //
    }

    public sealed class PurchaseOrderClassMap : CsvClassMap<PurchaseOrderFile>
    {
        public PurchaseOrderClassMap()
        {
            Map(u => u.OrderNumber).Index(0);
            Map(u => u.OrderDate).Index(1);
            Map(u => u.VendorName).Index(2);
            Map(u => u.ProductName).Index(3);
            Map(u => u.Description).Index(4);
            Map(u => u.ProductType).Index(5);
            Map(u => u.Model).Index(6);
            Map(u => u.Manufacturer).Index(7);
            Map(u => u.Quantity).Index(8);
            Map(u => u.PurchasePrice).Index(9);
            Map(u => u.FundingSource).Index(10);
            Map(u => u.AccountCode).Index(11);
            Map(u => u.LineNumber).Index(12);
            Map(u => u.ShippedToSite).Index(13);
            Map(u => u.QuantityShipped).Index(14);
            Map(u => u.Notes).Index(15);
        }
    }
}
