using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechAccessoryCharges
    {
        public int AccessoryChargeUid { get; set; }
        public int ChargeUid { get; set; }
        public int InventoryHistoryUid { get; set; }
        public int AccessoryUid { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public TblTechAccessories AccessoryU { get; set; }
        public TblUnvCharges ChargeU { get; set; }
        public TblTechInventoryHistory InventoryHistoryU { get; set; }
    }
}
