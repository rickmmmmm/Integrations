using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblDigitalMaterialDetails
    {
        public int DigitalMaterialDetailUid { get; set; }
        public int MaterialTypeId { get; set; }
        public int BookInventoryUid { get; set; }
        public string Product { get; set; }
        public string Website { get; set; }
        public int UsageTypeUid { get; set; }
        public int? Quantity { get; set; }
        public int? Duration { get; set; }
        public int DurationIntervalUid { get; set; }

        public TblBookInventory BookInventoryU { get; set; }
        public TblDurationIntervals DurationIntervalU { get; set; }
        public TblUsageTypes UsageTypeU { get; set; }
    }
}
