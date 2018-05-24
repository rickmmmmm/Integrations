using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechAttachmentScheduleLink
    {
        public int AttachmentScheduleLinkUid { get; set; }
        public int AttachmentUid { get; set; }
        public int ScheduleUid { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public TblTechAttachments AttachmentU { get; set; }
        public TblUnvSchedule ScheduleU { get; set; }
    }
}
