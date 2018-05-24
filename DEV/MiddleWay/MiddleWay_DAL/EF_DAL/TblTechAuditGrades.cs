using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechAuditGrades
    {
        public int AuditGradesUid { get; set; }
        public int AuditUid { get; set; }
        public string GradeName { get; set; }

        public TblUnvAudits AuditU { get; set; }
    }
}
