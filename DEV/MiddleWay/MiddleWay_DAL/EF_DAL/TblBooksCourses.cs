using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblBooksCourses
    {
        public int BooksCoursesUid { get; set; }
        public int CampusUid { get; set; }
        public int BookInventoryUid { get; set; }
        public int MasterCourseUid { get; set; }
        public int? Students { get; set; }
        public int? Teachers { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblBookInventory BookInventoryU { get; set; }
        public TblCampuses CampusU { get; set; }
        public TblMasterCourse MasterCourseU { get; set; }
    }
}
