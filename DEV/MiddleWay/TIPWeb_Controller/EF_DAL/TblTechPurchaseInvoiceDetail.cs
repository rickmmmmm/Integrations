using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechPurchaseInvoiceDetail
    {
        public int PurchaseInvoiceDetailUid { get; set; }
        public int PurchaseInvoiceUid { get; set; }
        public string LineNumber { get; set; }
        public string LineDescription { get; set; }
        public string AssetPrice { get; set; }
        public string InvoicePrice { get; set; }
        public string Quantity { get; set; }
        public string LineAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedBy { get; set; }

        public TblTechPurchaseInvoice PurchaseInvoiceU { get; set; }
    }
}
