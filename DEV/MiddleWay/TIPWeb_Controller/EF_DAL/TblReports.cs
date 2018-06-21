using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblReports
    {
        public int RecordId { get; set; }
        public int Category { get; set; }
        public string ReportName { get; set; }
        public string CampusDistrict { get; set; }
        public string Scope { get; set; }
        public string UserNotes { get; set; }
        public string ReportDescription { get; set; }
        public string ReportQuery { get; set; }
        public string SortQuery { get; set; }
        public string QuickReportsPage { get; set; }
        public int ReportId { get; set; }
    }
}
