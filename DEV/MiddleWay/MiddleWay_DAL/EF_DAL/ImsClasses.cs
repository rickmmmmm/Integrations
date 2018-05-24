using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class ImsClasses
    {
        public int ImsClassId { get; set; }
        public string CourseId { get; set; }
        public string CampusId { get; set; }
        public string ClassId { get; set; }
        public string ClassName { get; set; }
        public string Period { get; set; }
        public string Section { get; set; }
        public string RoomLocation { get; set; }
        public bool? Import { get; set; }
        public string RejectMessage { get; set; }
        public int? MasterCourseUid { get; set; }
        public int? CampusUid { get; set; }
        public int? CampusCourseAssignedUid { get; set; }
    }
}
