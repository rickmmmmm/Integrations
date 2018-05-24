using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechAuditDetailInventoryCounts
    {
        public TblTechAuditDetailInventoryCounts()
        {
            InverseFoundReferenceAuditDetailInventoryU = new HashSet<TblTechAuditDetailInventoryCounts>();
        }

        public int AuditDetailInventoryCountUid { get; set; }
        public int AuditDetailUid { get; set; }
        public int ItemUid { get; set; }
        public int InventoryUid { get; set; }
        public int StatusId { get; set; }
        public int ActionUid { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int? AuditSiteUid { get; set; }
        public int? OriginalStatusId { get; set; }
        public int? FoundReferenceAuditDetailInventoryUid { get; set; }
        public int? AuditEntityTypeUid { get; set; }
        public int? AuditEntityUid { get; set; }
        public DateTime? ScanDate { get; set; }
        public int? ScanBy { get; set; }
        public int? ScanByEntityTypeUid { get; set; }
        public int? ScanByEntityUid { get; set; }
        public int? LastModifiedByEntityUid { get; set; }
        public int? LastModifiedByEntityTypeUid { get; set; }
        public int? CreatedByEntityUid { get; set; }
        public int? CreatedByEntityTypeUid { get; set; }

        public TblTechActions ActionU { get; set; }
        public TblTechAuditDetails AuditDetailU { get; set; }
        public TblTechSites AuditSiteU { get; set; }
        public TblTechAuditDetailInventoryCounts FoundReferenceAuditDetailInventoryU { get; set; }
        public TblTechInventory InventoryU { get; set; }
        public TblTechItems ItemU { get; set; }
        public TblStatus OriginalStatus { get; set; }
        public TblStatus Status { get; set; }
        public ICollection<TblTechAuditDetailInventoryCounts> InverseFoundReferenceAuditDetailInventoryU { get; set; }
    }
}
