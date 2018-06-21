using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUnvRecipient
    {
        public TblUnvRecipient()
        {
            TblTechDepartments = new HashSet<TblTechDepartments>();
            TblTechStatusApprovalSettings = new HashSet<TblTechStatusApprovalSettings>();
            TblTechStatusChangeSettings = new HashSet<TblTechStatusChangeSettings>();
            TblTechTransferApproverExceptionDetails = new HashSet<TblTechTransferApproverExceptionDetails>();
            TblTechTransferDepartmentWorkflows = new HashSet<TblTechTransferDepartmentWorkflows>();
            TblTechTransferStatusWorkflowSettings = new HashSet<TblTechTransferStatusWorkflowSettings>();
            TblTechTransferWorkflowHistory = new HashSet<TblTechTransferWorkflowHistory>();
            TblTechTransferWorkflows = new HashSet<TblTechTransferWorkflows>();
            TblUnvRecipientInformation = new HashSet<TblUnvRecipientInformation>();
            TblUnvSchedule = new HashSet<TblUnvSchedule>();
        }

        public int RecipientUid { get; set; }
        public int ApplicationUid { get; set; }
        public int RecipientTypeUid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }

        public TblUnvApplications ApplicationU { get; set; }
        public TblUser CreatedByUser { get; set; }
        public TblUnvRecipientType RecipientTypeU { get; set; }
        public ICollection<TblTechDepartments> TblTechDepartments { get; set; }
        public ICollection<TblTechStatusApprovalSettings> TblTechStatusApprovalSettings { get; set; }
        public ICollection<TblTechStatusChangeSettings> TblTechStatusChangeSettings { get; set; }
        public ICollection<TblTechTransferApproverExceptionDetails> TblTechTransferApproverExceptionDetails { get; set; }
        public ICollection<TblTechTransferDepartmentWorkflows> TblTechTransferDepartmentWorkflows { get; set; }
        public ICollection<TblTechTransferStatusWorkflowSettings> TblTechTransferStatusWorkflowSettings { get; set; }
        public ICollection<TblTechTransferWorkflowHistory> TblTechTransferWorkflowHistory { get; set; }
        public ICollection<TblTechTransferWorkflows> TblTechTransferWorkflows { get; set; }
        public ICollection<TblUnvRecipientInformation> TblUnvRecipientInformation { get; set; }
        public ICollection<TblUnvSchedule> TblUnvSchedule { get; set; }
    }
}
