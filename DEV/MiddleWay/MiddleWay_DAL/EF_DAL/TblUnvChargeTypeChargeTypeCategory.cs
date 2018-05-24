using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUnvChargeTypeChargeTypeCategory
    {
        public int ChargeTypeChargeTypeCategoryUid { get; set; }
        public int ChargeTypeUid { get; set; }
        public int ChargeTypeCategoryUid { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblUnvChargeTypeCategories ChargeTypeCategoryU { get; set; }
        public TblUnvChargeTypes ChargeTypeU { get; set; }
    }
}
