using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechTransferDepartmentWorkflows
    {
        public int TransferDepartmentWorkflowUid { get; set; }
        public int TechDepartmentUid { get; set; }
        public int UserId { get; set; }
        public short ApprovalLevel { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CcnotificationRecipientUid { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedByUserId { get; set; }

        public TblUnvRecipient CcnotificationRecipientU { get; set; }
        public TblUser CreatedByUser { get; set; }
        public TblUser LastModifiedByUser { get; set; }
        public TblTechDepartments TechDepartmentU { get; set; }
        public TblUser User { get; set; }
    }
}
