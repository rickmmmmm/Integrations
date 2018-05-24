using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblAdjustment
    {
        public TblAdjustment()
        {
            FoundAdjustmentVendorOrders = new HashSet<FoundAdjustmentVendorOrders>();
        }

        public int AdjustmentId { get; set; }
        public string AdjustmentName { get; set; }
        public string Description1 { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserCreated { get; set; }
        public string Type { get; set; }
        public string AdjustmentStatus { get; set; }
        public string CampusId { get; set; }
        public bool CampusLocalCreated { get; set; }

        public ICollection<FoundAdjustmentVendorOrders> FoundAdjustmentVendorOrders { get; set; }
    }
}
