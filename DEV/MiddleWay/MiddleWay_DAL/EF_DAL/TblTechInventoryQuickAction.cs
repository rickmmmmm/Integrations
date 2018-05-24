using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechInventoryQuickAction
    {
        public int InventoryQuickActionUid { get; set; }
        public int InventoryUid { get; set; }
        public int SiteUid { get; set; }
        public int EntityUid { get; set; }
        public int EntityTypeUid { get; set; }
        public int ActionUid { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public TblTechActions ActionU { get; set; }
        public TblTechInventory InventoryU { get; set; }
        public TblTechSites SiteU { get; set; }
    }
}
