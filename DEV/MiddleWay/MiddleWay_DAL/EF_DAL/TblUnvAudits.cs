using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUnvAudits
    {
        public TblUnvAudits()
        {
            TblAuditBookInventory = new HashSet<TblAuditBookInventory>();
            TblAuditDetails = new HashSet<TblAuditDetails>();
            TblAuditFilterMaterialTypes = new HashSet<TblAuditFilterMaterialTypes>();
            TblAuditFilters = new HashSet<TblAuditFilters>();
            TblTechAuditDepartments = new HashSet<TblTechAuditDepartments>();
            TblTechAuditDetails = new HashSet<TblTechAuditDetails>();
            TblTechAuditEntityTypes = new HashSet<TblTechAuditEntityTypes>();
            TblTechAuditGrades = new HashSet<TblTechAuditGrades>();
            TblTechAuditItemTypes = new HashSet<TblTechAuditItemTypes>();
            TblTechAuditRoomTypes = new HashSet<TblTechAuditRoomTypes>();
            TblTechAuditStaffTypes = new HashSet<TblTechAuditStaffTypes>();
        }

        public int AuditUid { get; set; }
        public int ApplicationUid { get; set; }
        public int SiteUid { get; set; }
        public string AuditName { get; set; }
        public bool BlindAudit { get; set; }
        public bool ExcludeZeros { get; set; }
        public string AuditNotes { get; set; }
        public DateTime? ExcludeScannedAfterDate { get; set; }
        public bool DistrictCreated { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool RestrictSites { get; set; }
        public bool Approved { get; set; }
        public int? ApprovedByUserId { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public bool EmailHomeRoom { get; set; }
        public bool EmailStaff { get; set; }
        public DateTime? LastEmailSendDate { get; set; }
        public DateTime? AuditDueDate { get; set; }

        public TblUnvApplications ApplicationU { get; set; }
        public ICollection<TblAuditBookInventory> TblAuditBookInventory { get; set; }
        public ICollection<TblAuditDetails> TblAuditDetails { get; set; }
        public ICollection<TblAuditFilterMaterialTypes> TblAuditFilterMaterialTypes { get; set; }
        public ICollection<TblAuditFilters> TblAuditFilters { get; set; }
        public ICollection<TblTechAuditDepartments> TblTechAuditDepartments { get; set; }
        public ICollection<TblTechAuditDetails> TblTechAuditDetails { get; set; }
        public ICollection<TblTechAuditEntityTypes> TblTechAuditEntityTypes { get; set; }
        public ICollection<TblTechAuditGrades> TblTechAuditGrades { get; set; }
        public ICollection<TblTechAuditItemTypes> TblTechAuditItemTypes { get; set; }
        public ICollection<TblTechAuditRoomTypes> TblTechAuditRoomTypes { get; set; }
        public ICollection<TblTechAuditStaffTypes> TblTechAuditStaffTypes { get; set; }
    }
}
