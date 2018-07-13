using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddleWay_DTO.Enumerations;

namespace MiddleWay_DTO.Models.TIPWeb
{
    public class InventoryModel
    {
        public int InventoryUid { get; set; }
        public int InventoryTypeUid { get; set; }
        public int ItemUid { get; set; }
        public int SiteUid { get; set; }
        public int EntityUid { get; set; }
        public int EntityTypeUid { get; set; }
        public int StatusUid { get; set; }
        public int TechDepartmentUid { get; set; }
        public string Tag { get; set; }
        public string Serial { get; set; }
        public int FundingSourceUid { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string InventoryNotes { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int ArchiveUid { get; set; }
        public int ParentInventoryUid { get; set; }
        public string AssetId { get; set; }
        public bool BulkUpdated { get; set; }
        public int InventorySourceUid { get; set; }
        public int ContainerUid { get; set; }

    }

}
