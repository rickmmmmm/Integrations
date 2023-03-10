using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Model;
using Newtonsoft.Json;
using System.Configuration;
using Serilog;

namespace SystemTasks
{
    public class FileTasks
    {
        //Class implements all functionality for file tasks on csv files.
        public enum ImportType { PurchaseOrder, MobileDeviceManagement, InTouchPayments }

        public bool checkFile(string fileName)
        {
            try
            {
                var file = File.Open(fileName, FileMode.Open);
                file.Close();
                file.Dispose();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }

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

                int i = 0;

                while(i<3)
                {
                    reader.ReadLine();
                    i++;
                }

                var csv = new CsvReader(reader);

                csv.Configuration.RegisterClassMap<PurchaseOrderClassMap>();
                csv.Configuration.Delimiter = ConfigurationManager.AppSettings["delimiter"];
                csv.Configuration.Quote = ConfigurationManager.AppSettings["textQualifier"].ToCharArray()[0];
                csv.Configuration.TrimFields = true;
                while (csv.Read())
                {
                    Console.WriteLine("Getting record number " + csv.GetField<string>(ConfigurationManager.AppSettings["OrderNumber"]).Trim());

                    try {
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
                        newLine.PurchasePrice = ConfigurationManager.AppSettings["PurchasePrice"].IsValidMap() ? csv.GetField<decimal>(ConfigurationManager.AppSettings["PurchasePrice"]) : 0;
                        newLine.FundingSource = ConfigurationManager.AppSettings["FundingSource"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["FundingSource"]) : null;
                        newLine.AccountCode = ConfigurationManager.AppSettings["AccountCode"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["AccountCode"]) : null;
                        newLine.LineNumber = ConfigurationManager.AppSettings["LineNumber"].IsValidMap() ? csv.GetField<int>(ConfigurationManager.AppSettings["LineNumber"]) : 0;
                        newLine.ShippedToSite = ConfigurationManager.AppSettings["ShippedToSite"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["ShippedToSite"]) : ConfigurationManager.AppSettings["ShippedToSiteDefault"];
                        newLine.QuantityShipped = ConfigurationManager.AppSettings["QuantityShipped"].IsValidMap() ? csv.GetField<int>(ConfigurationManager.AppSettings["QuantityShipped"]) : 0;
                        newLine.Notes = ConfigurationManager.AppSettings["Notes"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["Notes"]) : "";
                    

                    if (string.IsNullOrEmpty(newLine.Model))
                    {
                        newLine.Model = "None";
                    }

                    payload.Add(newLine);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }

                return payload;

            }
        }

        public List<PurchaseOrderFile> serializeJsonFile(string filename)
        {
            using (StreamReader r = new StreamReader(filename))
            {
                string json = r.ReadToEnd().Replace("\r\n", "");
                dynamic array = JsonConvert.DeserializeObject(json);
                Console.WriteLine("checking");

                List<PurchaseOrderFile> orders = new List<PurchaseOrderFile>();

                foreach (var item in array)
                {
                    PurchaseOrderFile order = new PurchaseOrderFile();

                    string quantity = item[ConfigurationManager.AppSettings["Quantity"]].ToString().Trim() == "" ? "0.0" : item[ConfigurationManager.AppSettings["Quantity"]];
                    string price = item[ConfigurationManager.AppSettings["PurchasePrice"]].ToString().Trim() == "" ? "0.0" : item[ConfigurationManager.AppSettings["PurchasePrice"]];
                    string desc = ConfigurationManager.AppSettings["ProductName"].IsValidMap() ? item[ConfigurationManager.AppSettings["ProductName"]] : null;

                    order.OrderNumber = item[ConfigurationManager.AppSettings["OrderNumber"]];
                    order.OrderDate = ConfigurationManager.AppSettings["OrderDate"].IsValidMap() ? item[ConfigurationManager.AppSettings["OrderDate"]] : null;
                    order.VendorName = ConfigurationManager.AppSettings["VendorName"].IsValidMap() ? item[ConfigurationManager.AppSettings["VendorName"]] : null;
                    order.ProductName = desc != null && desc.Length > 100 ? desc.Substring(0, 99) : desc;
                    order.Description = ConfigurationManager.AppSettings["Description"].IsValidMap() ? item[ConfigurationManager.AppSettings["Description"]] : null;
                    order.ProductType = ConfigurationManager.AppSettings["ProductType"].IsValidMap() ? item[ConfigurationManager.AppSettings["ProductType"]] : ConfigurationManager.AppSettings["ProductTypeDefault"];
                    order.Model = ConfigurationManager.AppSettings["Model"].IsValidMap() ? item[ConfigurationManager.AppSettings["Model"]] : ConfigurationManager.AppSettings["ModelDefault"];
                    order.Manufacturer = ConfigurationManager.AppSettings["Manufacturer"].IsValidMap() ? item[ConfigurationManager.AppSettings["Manufacturer"]] : ConfigurationManager.AppSettings["ManufacturerDefault"];
                    order.Quantity = ConfigurationManager.AppSettings["Quantity"].IsValidMap() ? Convert.ToInt32(Convert.ToDecimal(quantity)) : 0;
                    order.PurchasePrice = ConfigurationManager.AppSettings["PurchasePrice"].IsValidMap() ? Convert.ToDecimal(price) : 0;
                    order.FundingSource = ConfigurationManager.AppSettings["FundingSource"].IsValidMap() ? item[ConfigurationManager.AppSettings["FundingSource"]] : null;
                    order.AccountCode = ConfigurationManager.AppSettings["AccountCode"].IsValidMap() ? item[ConfigurationManager.AppSettings["AccountCode"]] : null;
                    order.LineNumber = ConfigurationManager.AppSettings["LineNumber"].IsValidMap() && item[ConfigurationManager.AppSettings["LineNumber"]].ToString().Trim() != "" ? item[ConfigurationManager.AppSettings["LineNumber"]] : 0;
                    order.ShippedToSite = ConfigurationManager.AppSettings["ShippedToSite"].IsValidMap() ? item[ConfigurationManager.AppSettings["ShippedToSite"]] : ConfigurationManager.AppSettings["ShippedToSiteDefault"];
                    order.QuantityShipped = ConfigurationManager.AppSettings["QuantityShipped"].IsValidMap() ? Convert.ToInt32(Convert.ToDecimal(item[ConfigurationManager.AppSettings["QuantityShipped"]])) : 0;
                    order.Notes = ConfigurationManager.AppSettings["Notes"].IsValidMap() ? item[ConfigurationManager.AppSettings["Notes"]].ToString().Trim() : "";

                    if (string.IsNullOrEmpty(order.Model))
                    {
                        order.Model = "None";
                    }

                    orders.Add(order);
                }

                return orders;
            }
        }

        public void createExportFile(List<ReceivedTagsExportFile> results, string fileName)
        {
            using (StreamWriter writer = File.AppendText(fileName))
            {
                var csv = new CsvWriter(writer);
                csv.Configuration.Delimiter = ConfigurationManager.AppSettings["delimiter"];
                csv.Configuration.Quote = ConfigurationManager.AppSettings["textQualifier"].ToCharArray()[0];
                csv.Configuration.QuoteAllFields = true;

                csv.WriteRecords(results);
            }
        }

        public void createExportFile(List<ChargeExportFile> results, string fileName)
        {
            using (StreamWriter writer = File.AppendText(fileName))
            {
                var csv = new CsvWriter(writer);
                csv.Configuration.Delimiter = ConfigurationManager.AppSettings["delimiter"];
                csv.Configuration.IgnoreQuotes = true;
                csv.Configuration.HasHeaderRecord = false;
                //csv.Configuration.Quote = ConfigurationManager.AppSettings["textQualifier"].ToCharArray()[0];
                //csv.Configuration.QuoteAllFields = true;

                csv.WriteRecords(results);
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

        public void createRejectFile(string fileName, List<RejectedRecord> rejects, List<PurchaseOrderFile> payload)
        {
            foreach(var record in payload)
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

        public void archiveFile(string fileName)
        {
            string f = System.Guid.NewGuid().ToString();

            string newFile = fileName.Replace(".csv", "_processed_" + f + ".txt");

            File.Move(fileName, newFile);
        }

        public void copyToArchive(string file, string archiveLocation)
        {
            if (!Directory.Exists(archiveLocation))
            {
                Directory.CreateDirectory(archiveLocation);
            }

            var directory = Path.GetDirectoryName(file);

            var files = Directory.GetFiles(directory);

            if (files != null)
            {
                foreach (var archiveFile in files)
                {
                    var fileName = Path.GetFileName(archiveFile);
                    var destination = Path.Combine(archiveLocation, $"{DateTime.Now.ToString("yyyyMMdd")}_{fileName}");
                    File.Copy(archiveFile, destination, true);
                }
            }
        }

        public void moveToArchive(string file, string archiveLocation)
        {
            var destination = "";

            if (!Directory.Exists(archiveLocation))
            {
                Directory.CreateDirectory(archiveLocation);
            }

            var directory = Path.GetDirectoryName(file);

            var files = Directory.GetFiles(directory);

            if (files != null)
            {
                foreach (var archiveFile in files)
                {
                    var fileName = Path.GetFileName(archiveFile);
                    if (fileName == "Output.txt")
                    {
                        destination = Path.Combine(archiveLocation, $"{DateTime.Now.AddDays(-1).ToString("yyyyMMdd")}_{fileName}");
                    }
                    else
                    {
                        destination = Path.Combine(archiveLocation, $"{DateTime.Now.ToString("yyyyMMdd")}_{fileName}");
                    }

                    if (File.Exists(destination))
                    {
                        File.Delete(destination);
                    }

                    File.Move(archiveFile, destination);
                }
            }
        }

        public dynamic convertCsvFileToObject(string fileName, ImportType type)
        {
            switch (type)
            {
                case ImportType.PurchaseOrder:
                    return serializePurchaseOrderFile(fileName, true);
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
            Map(u => u.OrderNumber).Name(ConfigurationManager.AppSettings["OrderNumber"]);
            Map(u => u.OrderDate).Name(ConfigurationManager.AppSettings["OrderDate"]);
            Map(u => u.VendorName).Name(ConfigurationManager.AppSettings["VendorName"]);
            Map(u => u.ProductName).Name(ConfigurationManager.AppSettings["ProductName"]);
            Map(u => u.Description).Name(ConfigurationManager.AppSettings["Description"]);
            Map(u => u.ProductType).Name(ConfigurationManager.AppSettings["ProductType"]);
            Map(u => u.Model).Name(ConfigurationManager.AppSettings["Model"]);
            Map(u => u.Manufacturer).Name(ConfigurationManager.AppSettings["Manufacturer"]);
            Map(u => u.Quantity).Name(ConfigurationManager.AppSettings["Quantity"]);
            Map(u => u.PurchasePrice).Name(ConfigurationManager.AppSettings["PurchasePrice"]);
            Map(u => u.FundingSource).Name(ConfigurationManager.AppSettings["FundingSource"]);
            Map(u => u.AccountCode).Name(ConfigurationManager.AppSettings["AccountCode"]);
            Map(u => u.LineNumber).Name(ConfigurationManager.AppSettings["LineNumber"]);
            Map(u => u.ShippedToSite).Name(ConfigurationManager.AppSettings["ShippedToSite"]);
            Map(u => u.QuantityShipped).Name(ConfigurationManager.AppSettings["QuantityShipped"]);
            Map(u => u.Notes).Name(ConfigurationManager.AppSettings["Notes"]);
        }
    }
}
