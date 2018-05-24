using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechInventory
    {
        public TblTechInventory()
        {
            TblTechAuditDetailInventoryCounts = new HashSet<TblTechAuditDetailInventoryCounts>();
            TblTechInventoryDueDates = new HashSet<TblTechInventoryDueDates>();
            TblTechInventoryExt = new HashSet<TblTechInventoryExt>();
            TblTechInventoryHistory = new HashSet<TblTechInventoryHistory>();
            TblTechInventoryImports = new HashSet<TblTechInventoryImports>();
            TblTechInventoryInstallationDetails = new HashSet<TblTechInventoryInstallationDetails>();
            TblTechInventoryQuickAction = new HashSet<TblTechInventoryQuickAction>();
            TblTechInventoryStatusChangeRequests = new HashSet<TblTechInventoryStatusChangeRequests>();
            TblTechStudentInventory = new HashSet<TblTechStudentInventory>();
            TblTechTagHistory = new HashSet<TblTechTagHistory>();
            TblTechTransferInventory = new HashSet<TblTechTransferInventory>();
        }

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
        public decimal? PurchasePrice { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string InventoryNotes { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int ArchiveUid { get; set; }
        public int? ParentInventoryUid { get; set; }
        public string AssetId { get; set; }
        public bool BulkUpdated { get; set; }
        public int InventorySourceUid { get; set; }
        public int ContainerUid { get; set; }

        public TblUnvArchives ArchiveU { get; set; }
        public TblTechContainers ContainerU { get; set; }
        public TblUnvEntityTypes EntityTypeU { get; set; }
        public TblFundingSources FundingSourceU { get; set; }
        public TblTechInventorySource InventorySourceU { get; set; }
        public TblTechInventoryTypes InventoryTypeU { get; set; }
        public TblTechItems ItemU { get; set; }
        public TblTechSites SiteU { get; set; }
        public TblStatus StatusU { get; set; }
        public TblTechDepartments TechDepartmentU { get; set; }
        public TblTechInventoryDetails TblTechInventoryDetails { get; set; }
        public TblTechPurchaseInventory TblTechPurchaseInventory { get; set; }
        public ICollection<TblTechAuditDetailInventoryCounts> TblTechAuditDetailInventoryCounts { get; set; }
        public ICollection<TblTechInventoryDueDates> TblTechInventoryDueDates { get; set; }
        public ICollection<TblTechInventoryExt> TblTechInventoryExt { get; set; }
        public ICollection<TblTechInventoryHistory> TblTechInventoryHistory { get; set; }
        public ICollection<TblTechInventoryImports> TblTechInventoryImports { get; set; }
        public ICollection<TblTechInventoryInstallationDetails> TblTechInventoryInstallationDetails { get; set; }
        public ICollection<TblTechInventoryQuickAction> TblTechInventoryQuickAction { get; set; }
        public ICollection<TblTechInventoryStatusChangeRequests> TblTechInventoryStatusChangeRequests { get; set; }
        public ICollection<TblTechStudentInventory> TblTechStudentInventory { get; set; }
        public ICollection<TblTechTagHistory> TblTechTagHistory { get; set; }
        public ICollection<TblTechTransferInventory> TblTechTransferInventory { get; set; }
    }
}
