using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechItems
    {
        public TblTechItems()
        {
            TblTechAuditDetailInventoryCounts = new HashSet<TblTechAuditDetailInventoryCounts>();
            TblTechInventory = new HashSet<TblTechInventory>();
            TblTechItemAccessories = new HashSet<TblTechItemAccessories>();
            TblTechItemImages = new HashSet<TblTechItemImages>();
            TblTechPurchaseItemDetails = new HashSet<TblTechPurchaseItemDetails>();
            TblTechTransferRequestDetails = new HashSet<TblTechTransferRequestDetails>();
            TblTechUntaggedInventory = new HashSet<TblTechUntaggedInventory>();
        }

        public int ItemUid { get; set; }
        public string ItemNumber { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int ItemTypeUid { get; set; }
        public string ModelNumber { get; set; }
        public int ManufacturerUid { get; set; }
        public decimal? ItemSuggestedPrice { get; set; }
        public int AreaUid { get; set; }
        public string ItemNotes { get; set; }
        public string Sku { get; set; }
        public bool SerialRequired { get; set; }
        public int ProjectedLife { get; set; }
        public string CustomField1 { get; set; }
        public string CustomField2 { get; set; }
        public string CustomField3 { get; set; }
        public bool Active { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool AllowUntagged { get; set; }

        public TblUnvAreas AreaU { get; set; }
        public TblTechItemTypes ItemTypeU { get; set; }
        public TblUnvManufacturers ManufacturerU { get; set; }
        public ICollection<TblTechAuditDetailInventoryCounts> TblTechAuditDetailInventoryCounts { get; set; }
        public ICollection<TblTechInventory> TblTechInventory { get; set; }
        public ICollection<TblTechItemAccessories> TblTechItemAccessories { get; set; }
        public ICollection<TblTechItemImages> TblTechItemImages { get; set; }
        public ICollection<TblTechPurchaseItemDetails> TblTechPurchaseItemDetails { get; set; }
        public ICollection<TblTechTransferRequestDetails> TblTechTransferRequestDetails { get; set; }
        public ICollection<TblTechUntaggedInventory> TblTechUntaggedInventory { get; set; }
    }
}
