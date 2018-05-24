using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblAuditFilters
    {
        public int AuditFilterUid { get; set; }
        public int? AuditUid { get; set; }
        public string FilterType { get; set; }
        public string FilterValue { get; set; }

        public TblUnvAudits AuditU { get; set; }
    }
}
