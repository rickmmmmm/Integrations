using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechUserTypeWorkflows
    {
        public int UserTypeWorkflowUid { get; set; }
        public int UserTypeUid { get; set; }
        public int UserRoleTypeUid { get; set; }
        public int UserId { get; set; }
        public short ApprovalLevel { get; set; }
        public short SendEmailTo { get; set; }
        public string AdditionalEmail { get; set; }
        public int ApprovalUserTypeUid { get; set; }
        public int? TransferSiteUid { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblTechTransferSites TransferSiteU { get; set; }
        public TblUser User { get; set; }
        public TblTechUserRoleTypes UserRoleTypeU { get; set; }
        public TblUnvUserTypes UserTypeU { get; set; }
    }
}
