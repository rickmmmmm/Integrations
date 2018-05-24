using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblArchAdjustmentDetails
    {
        public int ArchiveId { get; set; }
        public int AdjustmentId { get; set; }
        public string Isbn { get; set; }
        public int CopiesToAdjust { get; set; }
        public int Posted { get; set; }
        public DateTime? DatePosted { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public DateTime? ArchivedDate { get; set; }
        public int? ArchivedUserId { get; set; }
    }
}
