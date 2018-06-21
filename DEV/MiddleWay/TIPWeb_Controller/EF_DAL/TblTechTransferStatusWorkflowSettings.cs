using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechTransferStatusWorkflowSettings
    {
        public int TransferStatusWorkflowSettingUid { get; set; }
        public int StatusId { get; set; }
        public int NotifyRecipientUid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedByUserId { get; set; }

        public TblUser CreatedByUser { get; set; }
        public TblUser LastModifiedByUser { get; set; }
        public TblUnvRecipient NotifyRecipientU { get; set; }
        public TblStatus Status { get; set; }
    }
}
