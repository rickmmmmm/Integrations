using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechInventoryExt
    {
        public int InventoryExtUid { get; set; }
        public int InventoryUid { get; set; }
        public int InventoryMetaUid { get; set; }
        public string InventoryExtValue { get; set; }

        public TblTechInventoryMeta InventoryMetaU { get; set; }
        public TblTechInventory InventoryU { get; set; }
    }
}
