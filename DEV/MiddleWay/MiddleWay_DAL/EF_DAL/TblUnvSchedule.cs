using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUnvSchedule
    {
        public TblUnvSchedule()
        {
            TblTechAttachmentScheduleLink = new HashSet<TblTechAttachmentScheduleLink>();
            TblTechScheduleReport = new HashSet<TblTechScheduleReport>();
            TblUnvScheduleDayAssigned = new HashSet<TblUnvScheduleDayAssigned>();
        }

        public int ScheduleUid { get; set; }
        public int ApplicationUid { get; set; }
        public int RecipientUid { get; set; }
        public int ScheduleTypeUid { get; set; }
        public int ScheduleReportTypeUid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? LastModifiedByUserId { get; set; }
        public DateTime? LastRunTime { get; set; }
        public bool Enabled { get; set; }

        public TblUser CreatedByUser { get; set; }
        public TblUser LastModifiedByUser { get; set; }
        public TblUnvRecipient RecipientU { get; set; }
        public TblUnvScheduleReportType ScheduleReportTypeU { get; set; }
        public TblUnvScheduleType ScheduleTypeU { get; set; }
        public ICollection<TblTechAttachmentScheduleLink> TblTechAttachmentScheduleLink { get; set; }
        public ICollection<TblTechScheduleReport> TblTechScheduleReport { get; set; }
        public ICollection<TblUnvScheduleDayAssigned> TblUnvScheduleDayAssigned { get; set; }
    }
}
