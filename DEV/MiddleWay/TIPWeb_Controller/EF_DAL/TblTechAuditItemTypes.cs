using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechAuditItemTypes
    {
        public int AuditItemTypeUid { get; set; }
        public int AuditUid { get; set; }
        public int ItemTypeUid { get; set; }

        public TblUnvAudits AuditU { get; set; }
        public TblTechItemTypes ItemTypeU { get; set; }
    }
}
