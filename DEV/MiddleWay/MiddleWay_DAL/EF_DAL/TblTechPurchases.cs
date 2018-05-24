using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechPurchases
    {
        public TblTechPurchases()
        {
            TblTechPurchaseInvoice = new HashSet<TblTechPurchaseInvoice>();
            TblTechPurchaseItemDetails = new HashSet<TblTechPurchaseItemDetails>();
        }

        public int PurchaseUid { get; set; }
        public int StatusUid { get; set; }
        public int VendorUid { get; set; }
        public int SiteUid { get; set; }
        public string OrderNumber { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string Other1 { get; set; }
        public string Frn { get; set; }

        public TblTechSites SiteU { get; set; }
        public TblStatus StatusU { get; set; }
        public TblVendor VendorU { get; set; }
        public ICollection<TblTechPurchaseInvoice> TblTechPurchaseInvoice { get; set; }
        public ICollection<TblTechPurchaseItemDetails> TblTechPurchaseItemDetails { get; set; }
    }
}
