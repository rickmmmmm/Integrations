using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay
{
    public class EtlShipmentsModel
    {
        public int _ETL_ShipmentsUid { get; set; }
        public int ProcessUid { get; set; }
        public int PurchaseItemShipmentUid { get; set; }
        public int PurchaseItemDetailUid { get; set; }
        public int OrderNumber { get; set; }
        public int LineNumber { get; set; }
        public int ShippedToSiteUid { get; set; }
        public int SiteId { get; set; }
        public int TicketNumber { get; set; }
        public int QuantityShipped { get; set; }
        public int TicketedByUserId { get; set; }
        public int TicketedBy { get; set; }
        public int TicketedDate { get; set; }
        public int StatusId { get; set; }
        public int Status { get; set; }
        public int InvoiceNumber { get; set; }
        public int InvoiceDate { get; set; }
        public int RowId { get; set; }
        public bool Rejected { get; set; }
        public string RejectedNotes { get; set; }
    }
}
