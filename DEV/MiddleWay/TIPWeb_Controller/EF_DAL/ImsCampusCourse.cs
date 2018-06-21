using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class ImsCampusCourse
    {
        public int ImsCampusCourseId { get; set; }
        public string CampusId { get; set; }
        public string CourseId { get; set; }
        public bool? Import { get; set; }
        public bool New { get; set; }
        public string RejectMessage { get; set; }
        public int? MasterCourseUid { get; set; }
        public int? CampusUid { get; set; }
        public int? CampusCoursesAssignedUid { get; set; }
        public string TeacherCount { get; set; }
        public string StudentCount { get; set; }
    }
}
