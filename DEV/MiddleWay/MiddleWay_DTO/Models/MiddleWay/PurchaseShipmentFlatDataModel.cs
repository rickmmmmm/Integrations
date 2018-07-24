using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay
{
    public class PurchaseShipmentFlatDataModel
    {
        public int PurchaseShipmentFlatDataUid { get; set; }
        public int ProcessTaskUid { get; set; }
        public int RowId { get; set; }
        public string OrderNumber { get; set; }
        public string LineNumber { get; set; }
        public string ShippedToSiteId { get; set; }
        public string ShippedToSiteName { get; set; }
        public string ShippedToSiteAddress { get; set; }
        public string ShippedToSiteCity { get; set; }
        public string ShippedToSiteState { get; set; }
        public string ShippedToSiteZip { get; set; }
        public string TicketNumber { get; set; }
        public string QuantityShipped { get; set; }
        public string TicketedBy { get; set; }
        public string TicketedDate { get; set; }
        public string Status { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public bool Rejected { get; set; }
        public string RejectedNotes { get; set; }
    }
}
