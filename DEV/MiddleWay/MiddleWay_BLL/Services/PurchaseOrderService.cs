using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Model;
//using DataAccess;
using SystemTasks;
using MiddleWay_DTO.Models;
using MiddleWay_DTO.ServiceInterfaces;
using MiddleWay_DTO.RepositoryInterfaces;

namespace SystemTasks
{
    public class PurchaseOrderService : IPurchaseOrderService
    {

        #region Private Variables and Properties

        private IPurchaseOrderRepository _purchaseOrderRepository;

        #endregion Private Variables and Properties

        #region Constructor

        public PurchaseOrderService(IPurchaseOrderRepository purchaseOrderRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        #endregion Constructor

        public List<ItemsDto> mapToItems(List<PurchaseOrderDto> payload)
        {
            throw new NotImplementedException();
            //List<ItemsDto> items = new List<ItemsDto>();

            //foreach (var item in payload.GroupBy(grp => new { grp.ProductName, grp.ProductType, grp.Model, grp.Manufacturer, grp.PurchasePrice }))
            //{
            //    try
            //    {
            //        //ItemsModel oneItem = _repo.getItemFromName(item.Key.ProductName);
            //        //items.Add(oneItem);
            //        continue;
            //    }
            //    catch (Exception ex)
            //    {

            //        continue;
            //    }
            //}

            //return items;
        }

        public List<PurchaseOrderDto> mapPurchaseOrderHeaders(List<PurchaseOrderDto> payload)
        {
            throw new NotImplementedException();
            //var orders = new List<PurchaseOrderDto>();

            //foreach (var item in payload.GroupBy(u => new { u.OrderNumber, u.PurchaseDate, u.Notes, u.VendorName, u.ShippedToSiteID }))
            //{
            //    var order = new PurchaseOrderDto();

            //    var details = payload.Where(p => p.OrderNumber == item.Key.OrderNumber).ToList();

            //    order.OrderNumber = item.Key.OrderNumber;
            //    order.PurchaseDate = Convert.ToDateTime(item.Key.OrderDate);
            //    order.Notes = item.Key.Notes;
            //    //order.StatusUID = _repo.getStatusUID("open");
            //    //order.VendorUID = _repo.getVendorUIDFromName(item.Key.VendorName);
            //    //order.SiteID = _repo.getSiteUIDFromName(item.Key.ShippedToSite);
            //    order.CreatedByUserId = 0;
            //    order.CreatedDate = DateTime.Now;
            //    order.LastModifiedByUserId = 0;
            //    order.LastModifiedDate = DateTime.Now;
            //    order.PurchaseOrderDetails = new List<PurchaseItemDetailDto>();

            //    foreach (var det in details)
            //    {
            //        order.PurchaseOrderDetails.Add(new PurchaseItemDetailDto
            //        {
            //            ParentPurchase = order,
            //            //ItemUID = _repo.getItemUIDFromName(det.ProductName),
            //            //FundingSourceUID = _repo.getFundingSourceUIDFromName(det.FundingSource),
            //            //StatusUID = _repo.getStatusUID("open"),
            //            //SiteAddedSiteUID = _repo.getSiteUIDFromName(det.ShippedToSite),
            //            QuantityOrdered = det.Quantity,
            //            QuantityReceived = det.Quantity,
            //            PurchasePrice = det.PurchasePrice,
            //            AccountCode = det.AccountCode,
            //            CreatedByUserID = 0,
            //            CreatedDate = DateTime.Now,
            //            LastModifiedByUserID = 0,
            //            LastModifiedDate = DateTime.Now,
            //            LineNumber = det.LineNumber
            //        }
            //        );
            //    }
            //    orders.Add(order);
            //}

            //return orders;
        }

        private List<PurchaseOrderDto> serializePurchaseOrderFile(string fileName)
        {
            throw new NotImplementedException();
            //using (StreamReader reader = File.OpenText(fileName))
            //{
            //    var csv = new CsvReader(reader);

            //    csv.Configuration.RegisterClassMap<PurchaseOrderClassMap>();
            //    csv.Configuration.Delimiter = ConfigurationManager.AppSettings["delimiter"];

            //    var payload = csv.GetRecords<PurchaseOrderFile>().ToList();

            //    return payload;
            //}


        }

        private List<PurchaseOrderDto> serializePurchaseOrderFile(string fileName, bool isManualMap)
        {
            throw new NotImplementedException();
            //using (StreamReader reader = File.OpenText(fileName))
            //{
            //    var payload = new List<PurchaseOrderFile>();
            //    var csv = new CsvReader(reader);

            //    csv.Configuration.RegisterClassMap<PurchaseOrderClassMap>();
            //    csv.Configuration.Delimiter = ConfigurationManager.AppSettings["delimiter"];
            //    csv.Configuration.Quote = ConfigurationManager.AppSettings["textQualifier"].ToCharArray()[0];
            //    //csv.Configuration.IgnoreQuotes = true;

            //    while (csv.Read())
            //    {

            //        PurchaseOrderFile newLine = new PurchaseOrderFile();

            //        newLine.OrderNumber = csv.GetField<string>(ConfigurationManager.AppSettings["OrderNumber"]).Trim();
            //        newLine.OrderDate = ConfigurationManager.AppSettings["OrderDate"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["OrderDate"]) : null;
            //        newLine.VendorName = ConfigurationManager.AppSettings["VendorName"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["VendorName"]) : null;
            //        newLine.ProductName = ConfigurationManager.AppSettings["ProductName"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["ProductName"]).Truncate(100) : null;
            //        newLine.Description = ConfigurationManager.AppSettings["Description"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["Description"]) : null;
            //        newLine.ProductType = ConfigurationManager.AppSettings["ProductType"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["ProductType"]) : ConfigurationManager.AppSettings["ProductTypeDefault"];
            //        newLine.Model = ConfigurationManager.AppSettings["Model"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["Model"]) : ConfigurationManager.AppSettings["ModelDefault"];
            //        newLine.Manufacturer = ConfigurationManager.AppSettings["Manufacturer"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["Manufacturer"]) : ConfigurationManager.AppSettings["ManufacturerDefault"];
            //        newLine.Quantity = ConfigurationManager.AppSettings["Quantity"].IsValidMap() ? Convert.ToInt32(csv.GetField<decimal>(ConfigurationManager.AppSettings["Quantity"])) : 0;
            //        newLine.PurchasePrice = ConfigurationManager.AppSettings["PurchasePrice"].IsValidMap() ? Convert.ToDecimal(csv.GetField<string>(ConfigurationManager.AppSettings["PurchasePrice"]).Replace("$", "")) : 0;
            //        newLine.FundingSource = ConfigurationManager.AppSettings["FundingSource"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["FundingSource"]) : null;
            //        newLine.AccountCode = ConfigurationManager.AppSettings["AccountCode"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["AccountCode"]) : null;
            //        newLine.LineNumber = ConfigurationManager.AppSettings["LineNumber"].IsValidMap() ? csv.GetField<int>(ConfigurationManager.AppSettings["LineNumber"]) : 0;
            //        newLine.ShippedToSite = ConfigurationManager.AppSettings["ShippedToSite"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["ShippedToSite"]) : ConfigurationManager.AppSettings["ShippedToSiteDefault"];
            //        newLine.QuantityShipped = ConfigurationManager.AppSettings["QuantityShipped"].IsValidMap() ? csv.GetField<int>(ConfigurationManager.AppSettings["QuantityShipped"]) : 0;
            //        newLine.Notes = ConfigurationManager.AppSettings["Notes"].IsValidMap() ? csv.GetField<string>(ConfigurationManager.AppSettings["Notes"]) : "";

            //        newLine.PurchasePrice = newLine.PurchasePrice / newLine.Quantity;

            //        payload.Add(newLine);
            //    }

            //    return payload;
            //}
            return null;
        }

        public void createRejectFile(string fileName, List<RejectedRecord> rejects, List<PurchaseOrderDto> payload)
        {
            throw new NotImplementedException();
            //foreach (var record in payload)
            //{
            //    if (rejects.Contains(rejects.Where(u => u.orderNumber == record.OrderNumber && u.LineNumber == record.LineNumber).FirstOrDefault()))
            //    {

            //        record.Accepted = "Rejected";
            //        record.Reason = rejects.Where(u => u.orderNumber == record.OrderNumber && u.LineNumber == record.LineNumber).FirstOrDefault().rejectReason;
            //    }

            //    else
            //    {
            //        record.Accepted = "Accepted";
            //    }
            //}

            //using (StreamWriter writer = File.AppendText(fileName))
            //{
            //    var csv = new CsvWriter(writer);
            //    csv.Configuration.Delimiter = ConfigurationManager.AppSettings["delimiter"];
            //    //csv.Configuration.Quote = ConfigurationManager.AppSettings["textQualifier"].ToCharArray()[0];
            //    csv.Configuration.QuoteAllFields = true;

            //    csv.WriteRecords(payload);
            //}
        }


        public List<PurchaseOrderDto> convertCsvFileToObject(string fileName)
        {
            return serializePurchaseOrderFile(fileName, true);
        }
        //TODO Add event handlers to log activity
        //TODO Add event handler for errors

        public void mapToInventoryHeaders(List<PurchaseOrderDto> payload)
        {
            throw new NotImplementedException();
        }


        #region Get Functions

        #endregion Get Functions

        #region Add Functions

        #endregion Add Functions

        #region Change Functions

        #endregion Change Functions

        #region Remove Functions

        #endregion Remove Functions
    }
}
