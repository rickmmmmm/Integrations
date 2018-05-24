using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechPurchaseItemShipments
    {
        public TblTechPurchaseItemShipments()
        {
            TblTechPurchaseInventory = new HashSet<TblTechPurchaseInventory>();
        }

        public int PurchaseItemShipmentUid { get; set; }
        public int PurchaseItemDetailUid { get; set; }
        public int ShippedToSiteUid { get; set; }
        public int? TicketNumber { get; set; }
        public int QuantityShipped { get; set; }
        public int? TicketedByUserId { get; set; }
        public DateTime? TicketedDate { get; set; }
        public int StatusUid { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }

        public TblTechPurchaseItemDetails PurchaseItemDetailU { get; set; }
        public TblTechSites ShippedToSiteU { get; set; }
        public TblStatus StatusU { get; set; }
        public ICollection<TblTechPurchaseInventory> TblTechPurchaseInventory { get; set; }
    }
}
