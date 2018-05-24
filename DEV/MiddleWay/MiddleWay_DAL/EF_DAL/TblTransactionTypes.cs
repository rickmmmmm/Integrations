using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTransactionTypes
    {
        public int RecordId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool DistrictCode { get; set; }
    }
}
