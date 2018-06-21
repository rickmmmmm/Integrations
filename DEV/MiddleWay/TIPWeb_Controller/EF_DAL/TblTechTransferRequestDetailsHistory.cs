using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechTransferRequestDetailsHistory
    {
        public int TransferRequestDetailsHistoryUid { get; set; }
        public int TransferRequestDetailsUid { get; set; }
        public int RequestedQuantity { get; set; }
        public int QuantityToFulfill { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }

        public TblTechTransferRequestDetails TransferRequestDetailsU { get; set; }
    }
}
