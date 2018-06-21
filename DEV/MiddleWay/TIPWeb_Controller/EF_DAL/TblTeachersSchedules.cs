using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTeachersSchedules
    {
        public int TeachersSchedulesUid { get; set; }
        public int TeachersUid { get; set; }
        public int? MasterCourseUid { get; set; }
        public string SectionId { get; set; }
        public string Period { get; set; }
        public Guid? SectionSifguid { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ExitDate { get; set; }

        public TblMasterCourse MasterCourseU { get; set; }
        public TblTeachers TeachersU { get; set; }
    }
}
