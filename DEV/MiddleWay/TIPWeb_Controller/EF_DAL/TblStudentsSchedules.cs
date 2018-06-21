using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblStudentsSchedules
    {
        public int StudentsSchedulesUid { get; set; }
        public int StudentsUid { get; set; }
        public int MasterCourseUid { get; set; }
        public string Period { get; set; }
        public string SectionId { get; set; }
        public Guid? EnrollmentSifguid { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ExitDate { get; set; }

        public TblMasterCourse MasterCourseU { get; set; }
        public TblStudents StudentsU { get; set; }
    }
}
