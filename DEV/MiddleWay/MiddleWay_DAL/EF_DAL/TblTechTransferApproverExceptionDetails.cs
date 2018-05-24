using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechTransferApproverExceptionDetails
    {
        public int TransferApproverExceptionDetailsId { get; set; }
        public string ApproverFlag { get; set; }
        public string ApproverName { get; set; }
        public string ApproverEmail { get; set; }
        public int? CcnotificationRecipientUid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public TblUnvRecipient CcnotificationRecipientU { get; set; }
    }
}
