using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechTransferSites
    {
        public TblTechTransferSites()
        {
            TblTechTransferWorkflowHistory = new HashSet<TblTechTransferWorkflowHistory>();
            TblTechTransferWorkflows = new HashSet<TblTechTransferWorkflows>();
            TblTechUserTypeWorkflows = new HashSet<TblTechUserTypeWorkflows>();
        }

        public int TransferSiteUid { get; set; }
        public string TransferSiteName { get; set; }
        public string Description { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<TblTechTransferWorkflowHistory> TblTechTransferWorkflowHistory { get; set; }
        public ICollection<TblTechTransferWorkflows> TblTechTransferWorkflows { get; set; }
        public ICollection<TblTechUserTypeWorkflows> TblTechUserTypeWorkflows { get; set; }
    }
}
