using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.DTO_Models.TIPWeb
{
    public class InventoryDto
    {
        public int InventoryId { get; set; }
        public int InventoryTypeId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int SiteUid { get; set; }
        public string SiteId { get; set; }
        public int EntityUid { get; set; }
        public string EntityId { get; set; }
        public string EntityName { get; set; }
        public int EntityTypeUid { get; set; }
        public int EntityTypeName { get; set; }
        public int StatusId { get; set; }
        public int DepartmentId { get; set; }
        public string Tag { get; set; }
        public string Serial { get; set; }
        public int FundingSourceId { get; set; }
        public string FundingSource { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string InventoryNotes { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int ArchiveId { get; set; }
        public DateTime ArchiveDate { get; set; }
        public int ParentInventoryId { get; set; }
        public string ParentTag { get; set; }
        public string AssetId { get; set; }
        public bool BulkUpdated { get; set; }
        public int InventorySourceId { get; set; }
        public int ContainerId { get; set; }
    }
}
