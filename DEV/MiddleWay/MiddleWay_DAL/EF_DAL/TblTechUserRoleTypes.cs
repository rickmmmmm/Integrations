using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechUserRoleTypes
    {
        public TblTechUserRoleTypes()
        {
            TblTechTransferWorkflowHistory = new HashSet<TblTechTransferWorkflowHistory>();
            TblTechTransferWorkflows = new HashSet<TblTechTransferWorkflows>();
            TblTechUserTypeWorkflows = new HashSet<TblTechUserTypeWorkflows>();
        }

        public int UserRoleTypeUid { get; set; }
        public string UserRoleTypeName { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<TblTechTransferWorkflowHistory> TblTechTransferWorkflowHistory { get; set; }
        public ICollection<TblTechTransferWorkflows> TblTechTransferWorkflows { get; set; }
        public ICollection<TblTechUserTypeWorkflows> TblTechUserTypeWorkflows { get; set; }
    }
}
