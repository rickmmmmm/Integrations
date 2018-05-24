using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblAdjustmentDetails
    {
        public int AdjustmentId { get; set; }
        public string Isbn { get; set; }
        public int? CopiesToAdjust { get; set; }
        public int? Posted { get; set; }
        public DateTime? DatePosted { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public int AdjustmentDetailsUid { get; set; }
    }
}
