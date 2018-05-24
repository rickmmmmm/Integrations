using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTransferDetails
    {
        public int RecordId { get; set; }
        public int TransferId { get; set; }
        public string Isbn { get; set; }
        public string SourceCampus { get; set; }
        public int CopiesReq { get; set; }
        public int CopiesSent { get; set; }
        public string Status { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedUser { get; set; }

        public TblTransfer Transfer { get; set; }
    }
}
