using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblArchCampusApproved
    {
        public string CampusId { get; set; }
        public string Isbn { get; set; }
        public int? StudentQuota { get; set; }
        public int? TeacherQuota { get; set; }
        public int UserId { get; set; }
        public DateTime? ArchivedDate { get; set; }
        public int? ArchivedUserId { get; set; }
        public int ArchiveId { get; set; }
    }
}
