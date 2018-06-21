using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUnvStaffTypes
    {
        public TblUnvStaffTypes()
        {
            TblTeachers = new HashSet<TblTeachers>();
            TblTechAuditStaffTypes = new HashSet<TblTechAuditStaffTypes>();
        }

        public int StaffTypeUid { get; set; }
        public string StaffTypeName { get; set; }
        public string StaffTypeDescription { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public ICollection<TblTeachers> TblTeachers { get; set; }
        public ICollection<TblTechAuditStaffTypes> TblTechAuditStaffTypes { get; set; }
    }
}
