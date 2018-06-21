using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechTransferUserWorkflows
    {
        public int TransferUserWorkflowUid { get; set; }
        public int UserId { get; set; }
        public int? ApproverUserId { get; set; }
        public string ApproverFullName { get; set; }
        public string ApproverEmail { get; set; }
        public short ApprovalLevel { get; set; }
        public string ApproverFlag { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedByUserId { get; set; }

        public TblUser ApproverUser { get; set; }
        public TblUser CreatedByUser { get; set; }
        public TblUser LastModifiedByUser { get; set; }
        public TblUser User { get; set; }
    }
}
