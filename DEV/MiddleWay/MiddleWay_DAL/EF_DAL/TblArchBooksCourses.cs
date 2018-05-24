using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblArchBooksCourses
    {
        public string CampusId { get; set; }
        public string Isbn { get; set; }
        public string CourseId { get; set; }
        public int? Students { get; set; }
        public int? Teachers { get; set; }
        public DateTime? ArchivedDate { get; set; }
        public int? ArchivedUserId { get; set; }
        public int ArchiveId { get; set; }
    }
}
