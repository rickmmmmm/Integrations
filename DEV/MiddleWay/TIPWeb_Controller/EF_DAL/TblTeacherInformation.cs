using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTeacherInformation
    {
        public int TeacherInformationUid { get; set; }
        public string TeacherId { get; set; }
        public string CurrentCampusId { get; set; }
        public bool Active { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
