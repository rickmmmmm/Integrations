using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay
{
    public class EtlShipmentsModel
    {
        public int _ETL_ShipmentsUID { get; set; }
        public int ProcessUid { get; set; }
        public int PurchaseItemShipmentUID { get; set; }
        public int PurchaseItemDetailUID { get; set; }
        public int OrderNumber { get; set; }
        public int LineNumber { get; set; }
        public int ShippedToSiteUID { get; set; }
        public int SiteID { get; set; }
        public int TicketNumber { get; set; }
        public int QuantityShipped { get; set; }
        public int TicketedByUserID { get; set; }
        public int TicketedBy { get; set; }
        public int TicketedDate { get; set; }
        public int StatusID { get; set; }
        public int Status { get; set; }
        public int InvoiceNumber { get; set; }
        public int InvoiceDate { get; set; }
    }
}
