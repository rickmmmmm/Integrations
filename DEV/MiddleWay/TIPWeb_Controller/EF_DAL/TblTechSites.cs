using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechSites
    {
        public TblTechSites()
        {
            TblTechAuditDetailInventoryCounts = new HashSet<TblTechAuditDetailInventoryCounts>();
            TblTechAuditDetails = new HashSet<TblTechAuditDetails>();
            TblTechInventory = new HashSet<TblTechInventory>();
            TblTechInventoryInstallationDetails = new HashSet<TblTechInventoryInstallationDetails>();
            TblTechInventoryQuickAction = new HashSet<TblTechInventoryQuickAction>();
            TblTechPurchaseItemDetails = new HashSet<TblTechPurchaseItemDetails>();
            TblTechPurchaseItemShipments = new HashSet<TblTechPurchaseItemShipments>();
            TblTechPurchases = new HashSet<TblTechPurchases>();
            TblTechUserSites = new HashSet<TblTechUserSites>();
            TblUser = new HashSet<TblUser>();
        }

        public int SiteUid { get; set; }
        public string SiteId { get; set; }
        public bool Active { get; set; }
        public string SiteName { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int SiteTypeUid { get; set; }
        public int? RegionId { get; set; }
        public string Notes { get; set; }
        public string ShippingAddress1 { get; set; }
        public string ShippingAddress2 { get; set; }
        public string ShippingCity { get; set; }
        public int? ShippingStateId { get; set; }
        public string ShippingZip { get; set; }
        public string ShippingInstructions { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingCity { get; set; }
        public int? BillingStateId { get; set; }
        public string BillingZip { get; set; }
        public string BillingInstructions { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public bool IsWarehouse { get; set; }
        public bool? IsDesignatedTransferSite { get; set; }
        public string FacilityId { get; set; }

        public TblRegion Region { get; set; }
        public TblTechSiteTypes SiteTypeU { get; set; }
        public ICollection<TblTechAuditDetailInventoryCounts> TblTechAuditDetailInventoryCounts { get; set; }
        public ICollection<TblTechAuditDetails> TblTechAuditDetails { get; set; }
        public ICollection<TblTechInventory> TblTechInventory { get; set; }
        public ICollection<TblTechInventoryInstallationDetails> TblTechInventoryInstallationDetails { get; set; }
        public ICollection<TblTechInventoryQuickAction> TblTechInventoryQuickAction { get; set; }
        public ICollection<TblTechPurchaseItemDetails> TblTechPurchaseItemDetails { get; set; }
        public ICollection<TblTechPurchaseItemShipments> TblTechPurchaseItemShipments { get; set; }
        public ICollection<TblTechPurchases> TblTechPurchases { get; set; }
        public ICollection<TblTechUserSites> TblTechUserSites { get; set; }
        public ICollection<TblUser> TblUser { get; set; }
    }
}
