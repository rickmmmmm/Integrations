using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUnvScheduleType
    {
        public TblUnvScheduleType()
        {
            TblUnvSchedule = new HashSet<TblUnvSchedule>();
            TblUnvScheduleDay = new HashSet<TblUnvScheduleDay>();
            TblUnvScheduleDayAssigned = new HashSet<TblUnvScheduleDayAssigned>();
        }

        public int ScheduleTypeUid { get; set; }
        public string ScheduleTypeName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public bool Enabled { get; set; }

        public TblUser CreatedByUser { get; set; }
        public ICollection<TblUnvSchedule> TblUnvSchedule { get; set; }
        public ICollection<TblUnvScheduleDay> TblUnvScheduleDay { get; set; }
        public ICollection<TblUnvScheduleDayAssigned> TblUnvScheduleDayAssigned { get; set; }
    }
}
