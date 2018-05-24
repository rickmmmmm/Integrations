using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblAuditDetails
    {
        public TblAuditDetails()
        {
            TblAuditDetailCounts = new HashSet<TblAuditDetailCounts>();
        }

        public int AuditDetailUid { get; set; }
        public int AuditUid { get; set; }
        public int CampusUid { get; set; }
        public int StatusId { get; set; }
        public DateTime? DueDate { get; set; }
        public int? SubmittedByUserId { get; set; }
        public DateTime? SubmitSignatureDateTime { get; set; }
        public string AuditDetailNotes { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string SubmitSignatureFullName { get; set; }
        public string SubmitSignatureInitials { get; set; }
        public string FinalizeSignatureFullName { get; set; }
        public string FinalizeSignatureInitials { get; set; }
        public DateTime? FinalizeSignatureDateTime { get; set; }

        public TblUnvAudits AuditU { get; set; }
        public TblCampuses CampusU { get; set; }
        public TblStatus Status { get; set; }
        public ICollection<TblAuditDetailCounts> TblAuditDetailCounts { get; set; }
    }
}
