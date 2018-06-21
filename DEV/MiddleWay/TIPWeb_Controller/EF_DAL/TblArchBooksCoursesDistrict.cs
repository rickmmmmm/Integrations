using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblArchBooksCoursesDistrict
    {
        public string Isbn { get; set; }
        public string CourseId { get; set; }
        public int? Students { get; set; }
        public int? Teachers { get; set; }
        public DateTime? ArchivedDate { get; set; }
        public int? ArchivedUserId { get; set; }
        public int ArchiveId { get; set; }
    }
}
