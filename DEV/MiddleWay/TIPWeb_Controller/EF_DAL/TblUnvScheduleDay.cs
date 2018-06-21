using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUnvScheduleDay
    {
        public TblUnvScheduleDay()
        {
            TblUnvScheduleDayAssigned = new HashSet<TblUnvScheduleDayAssigned>();
        }

        public int ScheduleDayUid { get; set; }
        public int ScheduleTypeUid { get; set; }
        public int ScheduleDay { get; set; }
        public string ScheduleDayName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }

        public TblUser CreatedByUser { get; set; }
        public TblUnvScheduleType ScheduleTypeU { get; set; }
        public ICollection<TblUnvScheduleDayAssigned> TblUnvScheduleDayAssigned { get; set; }
    }
}
