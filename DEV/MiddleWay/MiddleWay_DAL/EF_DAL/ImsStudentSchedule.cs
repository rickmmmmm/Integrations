using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class ImsStudentSchedule
    {
        public int ImsStudentScheduleId { get; set; }
        public string StudentId { get; set; }
        public string ClassId { get; set; }
        public string CourseId { get; set; }
        public string CampusId { get; set; }
        public string Period { get; set; }
        public string Section { get; set; }
        public bool? Import { get; set; }
        public string RejectMessage { get; set; }
        public int? StudentsUid { get; set; }
        public int? MasterCourseUid { get; set; }
        public int? StudentsSchedulesUid { get; set; }
        public int? CampusCoursesAssignedUid { get; set; }
        public bool New { get; set; }
    }
}
