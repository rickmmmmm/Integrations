using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechScheduleReport
    {
        public int ScheduleReportUid { get; set; }
        public int ScheduleUid { get; set; }
        public int ScheduleReportTypeUid { get; set; }
        public int? SearchUid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? LastModifiedByUserId { get; set; }

        public TblUser CreatedByUser { get; set; }
        public TblUser LastModifiedByUser { get; set; }
        public TblUnvScheduleReportType ScheduleReportTypeU { get; set; }
        public TblUnvSchedule ScheduleU { get; set; }
    }
}
