using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechUserDepartments
    {
        public int UserDepartmentUid { get; set; }
        public int UserId { get; set; }
        public int TechDepartmentUid { get; set; }
        public bool? ViewActive { get; set; }

        public TblTechDepartments TechDepartmentU { get; set; }
        public TblUser User { get; set; }
    }
}
