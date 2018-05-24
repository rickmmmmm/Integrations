using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class ImsMasterCourse
    {
        public int ImsMasterCourseId { get; set; }
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public bool? Import { get; set; }
        public bool New { get; set; }
        public string RejectMessage { get; set; }
        public int? MasterCourseUid { get; set; }
    }
}
