using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.Services
{
public    class PurchaseOrderService
    {

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

        private List<PurchaseOrderFile> serializePurchaseOrderFile(string fileName, bool isManualMap)
        {

            using (StreamReader reader = File.OpenText(fileName))
            {
                var payload = new List<PurchaseOrderFile>();
                var csv = new CsvReader(reader);

                csv.Configuration.RegisterClassMap<PurchaseOrderClassMap>();
                csv.Configuration.Delimiter = ConfigurationManager.AppSettings["delimiter"];
                csv.Configuration.Quote = ConfigurationManager.AppSettings["textQualifier"].ToCharArray()[0];
                //csv.Configuration.IgnoreQuotes = true;

                while (csv.Read())
                {

                    PurchaseOrderFile newLine = new PurchaseOrderFile();

                    newLine.OrderNumber = csv.GetField<string>(ConfigurationManager.AppSettings["OrderNumber"]).Trim();
                    newLine.OrderDate = ConfigurationManager.AppSettings["OrderDate"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["OrderDate"]) : null;
                    newLine.VendorName = ConfigurationManager.AppSettings["VendorName"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["VendorName"]) : null;
                    newLine.ProductName = ConfigurationManager.AppSettings["ProductName"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["ProductName"]).Truncate(100) : null;
                    newLine.Description = ConfigurationManager.AppSettings["Description"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["Description"]) : null;
                    newLine.ProductType = ConfigurationManager.AppSettings["ProductType"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["ProductType"]) : ConfigurationManager.AppSettings["ProductTypeDefault"];
                    newLine.Model = ConfigurationManager.AppSettings["Model"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["Model"]) : ConfigurationManager.AppSettings["ModelDefault"];
                    newLine.Manufacturer = ConfigurationManager.AppSettings["Manufacturer"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["Manufacturer"]) : ConfigurationManager.AppSettings["ManufacturerDefault"];
                    newLine.Quantity = ConfigurationManager.AppSettings["Quantity"].IsValidMap() ? Convert.ToInt32(csv.GetField<decimal>(ConfigurationManager.AppSettings["Quantity"])) : 0;
                    newLine.PurchasePrice = ConfigurationManager.AppSettings["PurchasePrice"].IsValidMap() ? Convert.ToDecimal(csv.GetField<string>(ConfigurationManager.AppSettings["PurchasePrice"]).Replace("$", "")) : 0;
                    newLine.FundingSource = ConfigurationManager.AppSettings["FundingSource"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["FundingSource"]) : null;
                    newLine.AccountCode = ConfigurationManager.AppSettings["AccountCode"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["AccountCode"]) : null;
                    newLine.LineNumber = ConfigurationManager.AppSettings["LineNumber"].IsValidMap() ? csv.GetField<int>(ConfigurationManager.AppSettings["LineNumber"]) : 0;
                    newLine.ShippedToSite = ConfigurationManager.AppSettings["ShippedToSite"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["ShippedToSite"]) : ConfigurationManager.AppSettings["ShippedToSiteDefault"];
                    newLine.QuantityShipped = ConfigurationManager.AppSettings["QuantityShipped"].IsValidMap() ? csv.GetField<int>(ConfigurationManager.AppSettings["QuantityShipped"]) : 0;
                    newLine.Notes = ConfigurationManager.AppSettings["Notes"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["Notes"]) : "";

                    newLine.PurchasePrice = newLine.PurchasePrice / newLine.Quantity;

                    payload.Add(newLine);
                }

                return payload;
            }


        }


        public void createRejectFile(string fileName, List<RejectedRecord> rejects, List<PurchaseOrderFile> payload)
        {
            foreach (var record in payload)
            {
                if (rejects.Contains(rejects.Where(u => u.orderNumber == record.OrderNumber && u.LineNumber == record.LineNumber).FirstOrDefault()))
                {

                    record.Accepted = "Rejected";
                    record.Reason = rejects.Where(u => u.orderNumber == record.OrderNumber && u.LineNumber == record.LineNumber).FirstOrDefault().rejectReason;
                }

                else
                {
                    record.Accepted = "Accepted";
                }
            }

            using (StreamWriter writer = File.AppendText(fileName))
            {
                var csv = new CsvWriter(writer);
                csv.Configuration.Delimiter = ConfigurationManager.AppSettings["delimiter"];
                //csv.Configuration.Quote = ConfigurationManager.AppSettings["textQualifier"].ToCharArray()[0];
                csv.Configuration.QuoteAllFields = true;

                csv.WriteRecords(payload);
            }
        }

        public List<PurchaseOrderFile> convertCsvFileToObject(string fileName)
        {
            return serializePurchaseOrderFile(fileName, true);
        }
    }
}
