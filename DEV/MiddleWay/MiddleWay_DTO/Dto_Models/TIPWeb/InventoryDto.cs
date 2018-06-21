using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.DTO_Models.TIPWeb
{
    public class InventoryDto
    {
        public int InventoryID { get; set; }
        public int InventoryTypeID { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int SiteUID { get; set; }
        public string SiteID { get; set; }
        public int EntityUID { get; set; }
        public string EntityID { get; set; }
        public string EntityName { get; set; }
        public int EntityTypeUID { get; set; }
        public int EntityTypeName { get; set; }
        public int StatusID { get; set; }
        public int DepartmentID { get; set; }
        public string Tag { get; set; }
        public string Serial { get; set; }
        public int FundingSourceID { get; set; }
        public string FundingSource { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string InventoryNotes { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int ArchiveID { get; set; }
        public DateTime ArchiveDate { get; set; }
        public int ParentInventoryID { get; set; }
        public string ParentTag { get; set; }
        public string AssetID { get; set; }
        public bool BulkUpdated { get; set; }
        public int InventorySourceID { get; set; }
        public int ContainerID { get; set; }
    }
}
