using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechTransferWorkflows
    {
        public TblTechTransferWorkflows()
        {
            TblTechTransferWorkflowLinks = new HashSet<TblTechTransferWorkflowLinks>();
        }

        public int TransferWorkflowUid { get; set; }
        public int TransferUid { get; set; }
        public int UserRoleTypeUid { get; set; }
        public int UserId { get; set; }
        public short ApprovalLevel { get; set; }
        public short SendEmailTo { get; set; }
        public string AdditionalEmail { get; set; }
        public string ApprovedByName { get; set; }
        public string ApprovedByUserType { get; set; }
        public int? ApprovedByUserId { get; set; }
        public string DeniedByName { get; set; }
        public string DeniedByUserType { get; set; }
        public int? DeniedByUserId { get; set; }
        public string OutsideUserName { get; set; }
        public int ApprovalUserTypeUid { get; set; }
        public int? TransferSiteUid { get; set; }
        public string ApproverFlag { get; set; }
        public int? CcnotificationRecipientUid { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblUnvRecipient CcnotificationRecipientU { get; set; }
        public TblTechTransferSites TransferSiteU { get; set; }
        public TblTechTransfers TransferU { get; set; }
        public TblUser User { get; set; }
        public TblTechUserRoleTypes UserRoleTypeU { get; set; }
        public ICollection<TblTechTransferWorkflowLinks> TblTechTransferWorkflowLinks { get; set; }
    }
}
