using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblAuditFilterMaterialTypes
    {
        public int AuditFilterMaterialTypesUid { get; set; }
        public int? AuditUid { get; set; }
        public int? MaterialTypeId { get; set; }

        public TblUnvAudits AuditU { get; set; }
    }
}
