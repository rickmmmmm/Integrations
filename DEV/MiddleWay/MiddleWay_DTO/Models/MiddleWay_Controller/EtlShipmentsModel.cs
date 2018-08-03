using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay_Controller
{
    public class EtlShipmentsModel
    {
        public int _ETL_ShipmentsUid { get; set; }
        public int ProcessTaskUid { get; set; }
        public int RowId { get; set; }
        public int PurchaseItemShipmentUid { get; set; }
        public int PurchaseItemDetailUid { get; set; }
        public string OrderNumber { get; set; }
        public int LineNumber { get; set; }
        public int ShippedToSiteUid { get; set; }
        public string ShippedToSiteId { get; set; }
        public string ShippedToSiteName { get; set; }
        public int? TicketNumber { get; set; }
        public int QuantityShipped { get; set; }
        public int? TicketedByUserId { get; set; }
        public string TicketedBy { get; set; }
        public DateTime? TicketedDate { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public bool Rejected { get; set; }
        public string RejectedNotes { get; set; }
    }
}
