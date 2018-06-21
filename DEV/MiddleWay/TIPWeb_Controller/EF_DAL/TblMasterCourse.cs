using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblMasterCourse
    {
        public TblMasterCourse()
        {
            TblBooksCourses = new HashSet<TblBooksCourses>();
            TblBooksCoursesDistrict = new HashSet<TblBooksCoursesDistrict>();
            TblCampusCoursesAssigned = new HashSet<TblCampusCoursesAssigned>();
            TblStudentsSchedules = new HashSet<TblStudentsSchedules>();
            TblTeachersSchedules = new HashSet<TblTeachersSchedules>();
        }

        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string Notes { get; set; }
        public int MasterCourseUid { get; set; }

        public ICollection<TblBooksCourses> TblBooksCourses { get; set; }
        public ICollection<TblBooksCoursesDistrict> TblBooksCoursesDistrict { get; set; }
        public ICollection<TblCampusCoursesAssigned> TblCampusCoursesAssigned { get; set; }
        public ICollection<TblStudentsSchedules> TblStudentsSchedules { get; set; }
        public ICollection<TblTeachersSchedules> TblTeachersSchedules { get; set; }
    }
}
