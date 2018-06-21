using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechActions
    {
        public TblTechActions()
        {
            TblTechAuditDetailInventoryCounts = new HashSet<TblTechAuditDetailInventoryCounts>();
            TblTechInventoryQuickAction = new HashSet<TblTechInventoryQuickAction>();
        }

        public int ActionUid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<TblTechAuditDetailInventoryCounts> TblTechAuditDetailInventoryCounts { get; set; }
        public ICollection<TblTechInventoryQuickAction> TblTechInventoryQuickAction { get; set; }
    }
}
