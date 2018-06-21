using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblAuditBookInventory
    {
        public int AuditBookInventoryUid { get; set; }
        public int AuditUid { get; set; }
        public int BookInventoryUid { get; set; }

        public TblUnvAudits AuditU { get; set; }
        public TblBookInventory BookInventoryU { get; set; }
    }
}
