using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUnvSignature
    {
        public TblUnvSignature()
        {
            TblTechTransferHistory = new HashSet<TblTechTransferHistory>();
        }

        public int SignatureUid { get; set; }
        public byte[] Signature { get; set; }
        public string PrintedName { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<TblTechTransferHistory> TblTechTransferHistory { get; set; }
    }
}
