using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechPurchaseItemDetails
    {
        public TblTechPurchaseItemDetails()
        {
            TblTechPurchaseItemShipments = new HashSet<TblTechPurchaseItemShipments>();
        }

        public int PurchaseItemDetailUid { get; set; }
        public int PurchaseUid { get; set; }
        public int ItemUid { get; set; }
        public int FundingSourceUid { get; set; }
        public int StatusUid { get; set; }
        public int SiteAddedSiteUid { get; set; }
        public int QuantityOrdered { get; set; }
        public int QuantityReceived { get; set; }
        public decimal PurchasePrice { get; set; }
        public string AccountCode { get; set; }
        public int TechDepartmentUid { get; set; }
        public int LineNumber { get; set; }
        public string Cfda { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsAssociated { get; set; }

        public TblFundingSources FundingSourceU { get; set; }
        public TblTechItems ItemU { get; set; }
        public TblTechPurchases PurchaseU { get; set; }
        public TblTechSites SiteAddedSiteU { get; set; }
        public TblStatus StatusU { get; set; }
        public TblTechDepartments TechDepartmentU { get; set; }
        public ICollection<TblTechPurchaseItemShipments> TblTechPurchaseItemShipments { get; set; }
    }
}
