using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblReportSorts
    {
        public int ReportId { get; set; }
        public string SortedBy { get; set; }
        public bool? DefaultSort { get; set; }
        public string DatabaseTranslation { get; set; }
    }
}
