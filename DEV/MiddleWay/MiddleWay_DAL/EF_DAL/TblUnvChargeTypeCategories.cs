using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUnvChargeTypeCategories
    {
        public TblUnvChargeTypeCategories()
        {
            TblUnvChargeTypeChargeTypeCategory = new HashSet<TblUnvChargeTypeChargeTypeCategory>();
        }

        public int ChargeTypeCategoryUid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public ICollection<TblUnvChargeTypeChargeTypeCategory> TblUnvChargeTypeChargeTypeCategory { get; set; }
    }
}
