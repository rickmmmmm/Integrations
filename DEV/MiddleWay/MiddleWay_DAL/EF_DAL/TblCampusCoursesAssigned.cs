using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblCampusCoursesAssigned
    {
        public int CampusCoursesAssignedUid { get; set; }
        public int CampusUid { get; set; }
        public int MasterCourseUid { get; set; }
        public long? StudentEnrollment { get; set; }
        public long? TeacherEnrollment { get; set; }
        public long? MaxStudentEnrollment { get; set; }
        public long? MaxTeacherEnrollment { get; set; }
        public Guid? CourseSifguid { get; set; }

        public TblCampuses CampusU { get; set; }
        public TblMasterCourse MasterCourseU { get; set; }
    }
}
