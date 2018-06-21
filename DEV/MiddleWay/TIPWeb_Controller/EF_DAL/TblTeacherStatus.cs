using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTeacherStatus
    {
        public TblTeacherStatus()
        {
            TblTeachers = new HashSet<TblTeachers>();
        }

        public int TeacherStatusUid { get; set; }
        public string TeacherStatusId { get; set; }
        public string TeacherStatus { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public ICollection<TblTeachers> TblTeachers { get; set; }
    }
}
