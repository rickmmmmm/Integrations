using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAccess;
using SystemTasks;

namespace SystemTasks
{
    public class PurchaseOrderMapping
    {
        private IRepository _repo;
        public PurchaseOrderMapping(IRepository repo)
        {
            _repo = repo;
        } 

        public List<Item> mapToItems(List<PurchaseOrderFile> payload)
        {
            List<Item> items = new List<Item>();

            foreach (var item in payload.GroupBy(grp => new { grp.ProductName, grp.ProductType, grp.Model, grp.Manufacturer, grp.PurchasePrice}))
            {
                try
                { 
                    Item oneItem = _repo.getItemFromName(item.Key.ProductName);
                    items.Add(oneItem);
                    continue;
                }
                catch (Exception ex)
                {

                    continue;
                }
            }

            return items;
        }

        public List<PurchaseOrderHeader> mapPurchaseOrderHeaders(List<PurchaseOrderFile> payload)
        {
            List<PurchaseOrderHeader> orders = new List<PurchaseOrderHeader>();

            foreach (var item in payload.GroupBy(u => new { u.OrderNumber, u.OrderDate, u.Notes, u.VendorName, u.ShippedToSite }))
            {
                PurchaseOrderHeader order = new PurchaseOrderHeader();

                var details = payload.Where(p => p.OrderNumber == item.Key.OrderNumber).ToList();

                order.PurchaseOrderNumber = item.Key.OrderNumber;
                order.PurchaseDate = item.Key.OrderDate.ToDateTimeFromString();
                order.Notes = item.Key.Notes;
                order.StatusUID = _repo.getStatusUID("open");
                order.VendorUID = _repo.getVendorUIDFromName(item.Key.VendorName);
                order.SiteID = _repo.getSiteUIDFromName(item.Key.ShippedToSite);
                order.CreatedByUserId = 0;
                order.CreatedDate = DateTime.Now;
                order.LastModifiedByUserId = 0;
                order.LastModifiedDate = DateTime.Now;
                order.PurchaseOrderDetails = new List<PurchaseOrderDetail>();

                foreach (var det in details)
                {
                    order.PurchaseOrderDetails.Add(new PurchaseOrderDetail
                    {
                        ParentPurchase = order,
                        ItemUID = _repo.getItemUIDFromName(det.ProductName),
                        FundingSourceUID = _repo.getFundingSourceUIDFromName(det.FundingSource),
                        StatusUID = _repo.getStatusUID("open"),
                        SiteAddedSiteUID = _repo.getSiteUIDFromName(det.ShippedToSite),
                        QuantityOrdered = Convert.ToInt32(det.Quantity),
                        PurchasePrice = det.PurchasePrice,
                        AccountCode = det.AccountCode,
                        CreatedByUserID = 0,
                        CreatedDate = DateTime.Now,
                        LastModifiedByUserID = 0,
                        LastModifiedDate = DateTime.Now,
                        LineNumber = det.LineNumber
                    }
                    );
                }
                orders.Add(order);
            }

            return orders;
        }

        //TODO Add event handlers to log activity
        //TODO Add event handler for errors

        public void mapToInventoryHeaders(List<PurchaseOrderFile> payload)
        {
            throw new NotImplementedException();
        }
    }
}
