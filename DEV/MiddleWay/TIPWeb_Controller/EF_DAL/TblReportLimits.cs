using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblReportLimits
    {
        public int ReportId { get; set; }
        public string LimitBy { get; set; }
        public bool? DefaultLimit { get; set; }
        public string DatabaseTranslation { get; set; }
    }
}
