using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTransfer
    {
        public TblTransfer()
        {
            TblTransferDetails = new HashSet<TblTransferDetails>();
        }

        public int TransferId { get; set; }
        public string TransferName { get; set; }
        public string TargetCampus { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserCreated { get; set; }
        public string Status { get; set; }
        public bool StudentTransfer { get; set; }
        public string Notes { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedUser { get; set; }
        public bool? Active { get; set; }

        public ICollection<TblTransferDetails> TblTransferDetails { get; set; }
    }
}
