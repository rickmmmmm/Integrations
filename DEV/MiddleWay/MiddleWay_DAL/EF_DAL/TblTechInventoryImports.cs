using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechInventoryImports
    {
        public int InventoryImportUid { get; set; }
        public int InventoryUid { get; set; }
        public int ImportUid { get; set; }

        public TblTechImports ImportU { get; set; }
        public TblTechInventory InventoryU { get; set; }
    }
}
