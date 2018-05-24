using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechTransferWorkflowLinks
    {
        public int TransferWorkflowLinkUid { get; set; }
        public int TransferWorkflowUid { get; set; }
        public int UserId { get; set; }
        public string EncryptedLink { get; set; }

        public TblTechTransferWorkflows TransferWorkflowU { get; set; }
        public TblUser User { get; set; }
    }
}
