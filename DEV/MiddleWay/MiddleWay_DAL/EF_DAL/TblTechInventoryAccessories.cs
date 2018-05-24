using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechInventoryAccessories
    {
        public int InventoryAccessoryUid { get; set; }
        public int InventoryHistoryUid { get; set; }
        public int AccessoryUid { get; set; }
        public int Issued { get; set; }
        public int Returned { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblTechAccessories AccessoryU { get; set; }
        public TblTechInventoryHistory InventoryHistoryU { get; set; }
    }
}
