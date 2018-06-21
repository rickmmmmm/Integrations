using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechInventoryMeta
    {
        public TblTechInventoryMeta()
        {
            TblTechInventoryExt = new HashSet<TblTechInventoryExt>();
        }

        public int InventoryMetaUid { get; set; }
        public int ItemTypeUid { get; set; }
        public string InventoryMetaLabel { get; set; }
        public string InventoryMetaType { get; set; }
        public bool InventoryMetaRequired { get; set; }
        public int InventoryMetaOrder { get; set; }

        public TblTechItemTypes ItemTypeU { get; set; }
        public ICollection<TblTechInventoryExt> TblTechInventoryExt { get; set; }
    }
}
