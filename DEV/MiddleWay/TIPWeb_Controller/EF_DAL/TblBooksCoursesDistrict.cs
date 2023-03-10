using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblBooksCoursesDistrict
    {
        public int BooksCoursesDistrictUid { get; set; }
        public int BookInventoryUid { get; set; }
        public int MasterCourseUid { get; set; }
        public int? Students { get; set; }
        public int? Teachers { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblBookInventory BookInventoryU { get; set; }
        public TblMasterCourse MasterCourseU { get; set; }
    }
}
