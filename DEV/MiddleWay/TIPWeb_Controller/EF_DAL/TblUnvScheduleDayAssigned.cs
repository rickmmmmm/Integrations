using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUnvScheduleDayAssigned
    {
        public int ScheduleDayAssignedUid { get; set; }
        public int ScheduleUid { get; set; }
        public int ScheduleTypeUid { get; set; }
        public int ScheduleDayUid { get; set; }
        public TimeSpan ScheduleTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? LastModifiedByUserId { get; set; }

        public TblUser CreatedByUser { get; set; }
        public TblUser LastModifiedByUser { get; set; }
        public TblUnvScheduleDay ScheduleDayU { get; set; }
        public TblUnvScheduleType ScheduleTypeU { get; set; }
        public TblUnvSchedule ScheduleU { get; set; }
    }
}
