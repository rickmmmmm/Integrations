﻿using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechAuditDepartments
    {
        public int AuditDepartmentUid { get; set; }
        public int AuditUid { get; set; }
        public int TechDepartmentUid { get; set; }

        public TblUnvAudits AuditU { get; set; }
        public TblTechDepartments TechDepartmentU { get; set; }
    }
}
