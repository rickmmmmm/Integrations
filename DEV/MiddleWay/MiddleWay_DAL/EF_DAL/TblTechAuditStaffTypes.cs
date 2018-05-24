using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechAuditStaffTypes
    {
        public int AuditStaffTypesUid { get; set; }
        public int AuditUid { get; set; }
        public int StaffTypeUid { get; set; }

        public TblUnvAudits AuditU { get; set; }
        public TblUnvStaffTypes StaffTypeU { get; set; }
    }
}
