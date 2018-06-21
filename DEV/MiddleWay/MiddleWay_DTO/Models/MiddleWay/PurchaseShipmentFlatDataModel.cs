using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay
{
    public class PurchaseShipmentFlatDataModel
    {
        public int PurchaseShipmentFlatDataUID { get; set; }
        public int ProcessUid { get; set; }
        public int OrderNumber { get; set; }
        public int LineNumber { get; set; }
        public int ShippedToSiteID { get; set; }
        public int ShippedToSiteName { get; set; }
        public int ShippedToSiteAddress { get; set; }
        public int ShippedToSiteCity { get; set; }
        public int ShippedToSiteState { get; set; }
        public int ShippedToSiteZip { get; set; }
        public int TicketNumber { get; set; }
        public int QuantityShipped { get; set; }
        public int TicketedBy { get; set; }
        public int TicketedDate { get; set; }
        public int Status { get; set; }
        public int InvoiceNumber { get; set; }
        public int InvoiceDate { get; set; }
    }
}
