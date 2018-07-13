using System;
using System.Collections.Generic;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public partial class PurchaseInvoiceFlatData
    {
        public int PurchaseInvoiceFlatDataUid { get; set; }
        public int ProcessUid { get; set; }
        public int RowId { get; set; }
        public string OrderNumber { get; set; }
        public string LineNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceStatus { get; set; }
        public string AuthorizationStatus { get; set; }
        public string AccountingDate { get; set; }
        public string LineDescription { get; set; }
        public string AssetPrice { get; set; }
        public string InvoicePrice { get; set; }
        public string Quantity { get; set; }
        public string LineAmount { get; set; }
        public bool Rejected { get; set; }
        public string RejectedNotes { get; set; }

        public Processes ProcessU { get; set; }
    }
}
