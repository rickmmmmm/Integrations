using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechAuditEntityTypes
    {
        public int AuditEntityTypeUid { get; set; }
        public int AuditUid { get; set; }
        public int EntityTypeUid { get; set; }

        public TblUnvAudits AuditU { get; set; }
        public TblUnvEntityTypes EntityTypeU { get; set; }
    }
}
