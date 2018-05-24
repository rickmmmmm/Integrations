using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechPurchaseInvoice
    {
        public TblTechPurchaseInvoice()
        {
            TblTechPurchaseInvoiceDetail = new HashSet<TblTechPurchaseInvoiceDetail>();
        }

        public int PurchaseInvoiceUid { get; set; }
        public int PurchaseUid { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceStatus { get; set; }
        public string AuthorizationStatus { get; set; }
        public string AccountingDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public string LastModifiedDate { get; set; }
        public int LastModifiedBy { get; set; }

        public TblTechPurchases PurchaseU { get; set; }
        public ICollection<TblTechPurchaseInvoiceDetail> TblTechPurchaseInvoiceDetail { get; set; }
    }
}
