using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechAuditDetails
    {
        public TblTechAuditDetails()
        {
            TblTechAuditDetailInventoryCounts = new HashSet<TblTechAuditDetailInventoryCounts>();
        }

        public int AuditDetailUid { get; set; }
        public int AuditUid { get; set; }
        public int SiteUid { get; set; }
        public int EntityTypeUid { get; set; }
        public int EntityUid { get; set; }
        public int StatusId { get; set; }
        public DateTime? DueDate { get; set; }
        public int? FinalizedByUserId { get; set; }
        public DateTime? FinalizedDate { get; set; }
        public string AuditDetailNotes { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int? FinalizedByEntityTypeUid { get; set; }
        public int? FinalizedByEntityUid { get; set; }
        public int? LastModifiedByEntityUid { get; set; }
        public int? LastModifiedByEntityTypeUid { get; set; }

        public TblUnvAudits AuditU { get; set; }
        public TblTechSites SiteU { get; set; }
        public TblStatus Status { get; set; }
        public ICollection<TblTechAuditDetailInventoryCounts> TblTechAuditDetailInventoryCounts { get; set; }
    }
}
