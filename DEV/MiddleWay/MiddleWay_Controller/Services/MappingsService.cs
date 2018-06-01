using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using MiddleWay_Controller.Interfaces;

namespace MiddleWay_Controller.Services
{
    public class MappingsService : IMappingsService
    {
    //}

    //public sealed class PurchaseOrderClassMap : ClassMap<PurchaseOrderFile>
    //{
        public MappingsService() //PurchaseOrderClassMap() //Convert to a more generic format
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
