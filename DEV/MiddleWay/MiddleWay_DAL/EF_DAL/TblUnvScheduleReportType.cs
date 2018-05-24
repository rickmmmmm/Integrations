using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUnvScheduleReportType
    {
        public TblUnvScheduleReportType()
        {
            TblTechScheduleReport = new HashSet<TblTechScheduleReport>();
            TblUnvSchedule = new HashSet<TblUnvSchedule>();
        }

        public int ScheduleReportTypeUid { get; set; }
        public string ScheduleReportTypeName { get; set; }
        public int ApplicationUid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }

        public TblUnvApplications ApplicationU { get; set; }
        public TblUser CreatedByUser { get; set; }
        public ICollection<TblTechScheduleReport> TblTechScheduleReport { get; set; }
        public ICollection<TblUnvSchedule> TblUnvSchedule { get; set; }
    }
}
