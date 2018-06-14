using System;
using System.Collections.Generic;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public partial class PurchaseInvoiceFlatData
    {
        public int PurchaseInvoiceFlatDataUid { get; set; }
        public int ProcessUid { get; set; }
        public string OrderNumber { get; set; }
        public int? LineNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceStatus { get; set; }
        public string AuthorizationStatus { get; set; }
        public string AccountingDate { get; set; }
        public string LineDescription { get; set; }
        public decimal? AssetPrice { get; set; }
        public decimal? InvoicePrice { get; set; }
        public int? Quantity { get; set; }
        public decimal? LineAmount { get; set; }

        public Processes ProcessU { get; set; }
    }
}
