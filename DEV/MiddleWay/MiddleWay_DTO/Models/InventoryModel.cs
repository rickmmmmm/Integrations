using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddleWay_DTO.Enumerations;

namespace MiddleWay_DTO.Models
{
    public class InventoryModel
    {
        public InventoryTypeEnum InventoryTypeUID { get; set; }
        public ItemsModel SiblingItem { get; set; }
        public int SiteUID { get; set; }
        public int EntityUID { get; set; }
        public EntityTypeEnum EntityTypeUID { get; set; }
        public int StatusUID { get; set; }
        public int TechDepartmentUID { get; set; }
        public string Tag { get; set; }
        public string Serial { get; set; }
        public int FundingSourceUID { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string InventoryNotes { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int ArchiveUID { get; set; }
        public int ParentInventoryUID { get; set; }
        public string AssetID { get; set; }
        public bool BulkUpdated { get; set; }
        public int InventorySourceUID { get; set; }
        public int ContainerUID { get; set; }

    }

}
