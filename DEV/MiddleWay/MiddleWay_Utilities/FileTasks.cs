using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
//using Model;
using System.Configuration;
using MiddleWay_DTO.Models;
using PapaParse.Net;
using System.Diagnostics;

namespace MiddleWay_Utilities
{
    public class FileTasks
    {
        //Class implements all functionality for file tasks on csv files.

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

        public List<Dictionary<string, string>> readCSV(string filename)
        {
            var rows = new List<Dictionary<string, string>>();

            var config = new PapaParse.Net.Config()
            {
                worker = true,
                header = true,
                skipEmptyLines = true,
                delimiter = ",",
                quoteChar = '\'',
                chunkSize = 5000
            };

            var parseHandler = new ParserHandle(config);

            var parseConfig = new Config()
            {
                worker = true,
                header = true,
                skipEmptyLines = true,
                delimiter = ",",
                quoteChar = '\'',
               // chunk = (result, parseHandle) =>
               // {
               //     Debug.WriteLine(result.dataWithHeader);
               // },
               // step = (result, parseHandle) =>
               //{
               //    Result a = result;
               //    //data.push(_.head(result.data));
               //    //if (result.errors.length) {
               //    //    console.log(`Row data: ${result.data}`);
               //    //    result.errors.map(error => {
               //    //        console.log(chalk.red(`Row error: ${error.message}`));
               //    //    });
               //    //}

               //},
                complete = (parsed) =>
                {
                    rows.AddRange(parsed.dataWithHeader);
                }
            };

            using (FileStream stream = File.OpenRead(filename))
            {
                Papa.parse(stream, parseConfig);
            }

            //string line;
            //using (var fileReader = new StreamReader(filename))
            //{
            //    while ((line = fileReader.ReadLine()) != null)
            //    {
            //        Papa.parse(line, parseConfig);
            //    }

            //    fileReader.Close();
            //}

            return rows;
        }

        // Received Tags Export.... (Read Option e)
        public void createExportFile(List<ReceivedTagsExportFile> results, string fileName, string delimiter, char textQualifier)
        {
            using (StreamWriter writer = File.AppendText(fileName))
            {
                var csv = new CsvWriter(writer);
                csv.Configuration.Delimiter = delimiter; // ConfigurationManager.AppSettings["delimiter"];
                csv.Configuration.Quote = textQualifier; //  ConfigurationManager.AppSettings["textQualifier"].ToCharArray()[0];
                csv.Configuration.QuoteAllFields = true;

                csv.WriteRecords(results);
            }
        }

        ///UNUSED
        //public void createRejectFile(string fileName, List<RejectedRecord> rejects)
        //{
        //    using (StreamWriter writer = File.AppendText(fileName))
        //    {
        //        var csv = new CsvWriter(writer);
        //        csv.Configuration.Delimiter = ConfigurationManager.AppSettings["delimiter"];
        //        csv.Configuration.Quote = ConfigurationManager.AppSettings["textQualifier"].ToCharArray()[0];
        //        csv.Configuration.QuoteAllFields = true;

        //        csv.WriteRecords(rejects);
        //    }
        //}



        public void archiveFile(string fileName)
        {
            string f = System.Guid.NewGuid().ToString();

            string newFile = fileName.Replace(".csv", "_processed_" + f + ".txt");

            File.Move(fileName, newFile);
        }

        //public dynamic convertCsvFileToObject(string fileName, ImportType type)
        //{
        //    switch (type)
        //    {
        //        case ImportType.PurchaseOrder:
        //            return serializePurchaseOrderFile(fileName, true);
        //        case ImportType.MobileDeviceManagement:
        //            return false;
        //    }

        //    return true;
        //}



        //TODO:
        //Convert file to object
        //
    }

    public sealed class PurchaseOrderClassMap : ClassMap<PurchaseOrderDto>
    {
        public PurchaseOrderClassMap()
        {
            //Map(u => u.OrderNumber).Name(ConfigurationManager.AppSettings["OrderNumber"]);
            //Map(u => u.OrderDate).Name(ConfigurationManager.AppSettings["OrderDate"]);
            //Map(u => u.VendorName).Name(ConfigurationManager.AppSettings["VendorName"]);
            //Map(u => u.ProductName).Name(ConfigurationManager.AppSettings["ProductName"]);
            //Map(u => u.Description).Name(ConfigurationManager.AppSettings["Description"]);
            //Map(u => u.ProductType).Name(ConfigurationManager.AppSettings["ProductType"]);
            //Map(u => u.Model).Name(ConfigurationManager.AppSettings["Model"]);
            //Map(u => u.Manufacturer).Name(ConfigurationManager.AppSettings["Manufacturer"]);
            //Map(u => u.Quantity).Name(ConfigurationManager.AppSettings["Quantity"]);
            //Map(u => u.PurchasePrice).Name(ConfigurationManager.AppSettings["PurchasePrice"]);
            //Map(u => u.FundingSource).Name(ConfigurationManager.AppSettings["FundingSource"]);
            //Map(u => u.AccountCode).Name(ConfigurationManager.AppSettings["AccountCode"]);
            //Map(u => u.LineNumber).Name(ConfigurationManager.AppSettings["LineNumber"]);
            //Map(u => u.ShippedToSite).Name(ConfigurationManager.AppSettings["ShippedToSite"]);
            //Map(u => u.QuantityShipped).Name(ConfigurationManager.AppSettings["QuantityShipped"]);
            //Map(u => u.Notes).Name(ConfigurationManager.AppSettings["Notes"]);



        }
    }
}
