using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class FoundAdjustmentVendorOrders
    {
        public int FoundAdjustmentVendorOrderUid { get; set; }
        public int FoundAdjustmentUid { get; set; }
        public int VendorOrderUid { get; set; }

        public TblAdjustment FoundAdjustmentU { get; set; }
        public TblVendorOrders VendorOrderU { get; set; }
    }
}
