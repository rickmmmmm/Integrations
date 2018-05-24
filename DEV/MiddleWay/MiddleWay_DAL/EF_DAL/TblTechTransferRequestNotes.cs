using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechTransferRequestNotes
    {
        public int TransferRequestNotesUid { get; set; }
        public int TransferUid { get; set; }
        public int StatusUid { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedByUserId { get; set; }

        public TblStatus StatusU { get; set; }
        public TblTechTransfers TransferU { get; set; }
    }
}
