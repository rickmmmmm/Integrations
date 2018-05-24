using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechBulkEdit
    {
        public int BulkEditUid { get; set; }
        public string Field { get; set; }
        public string Value { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
