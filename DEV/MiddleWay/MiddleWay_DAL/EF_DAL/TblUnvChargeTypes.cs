using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUnvChargeTypes
    {
        public TblUnvChargeTypes()
        {
            TblUnvChargeTypeChargeTypeCategory = new HashSet<TblUnvChargeTypeChargeTypeCategory>();
            TblUnvCharges = new HashSet<TblUnvCharges>();
        }

        public int ChargeTypeUid { get; set; }
        public int ApplicationUid { get; set; }
        public string Description { get; set; }
        public double ChargeAmount { get; set; }
        public bool Percentage { get; set; }
        public bool AllowPriceChanges { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public ICollection<TblUnvChargeTypeChargeTypeCategory> TblUnvChargeTypeChargeTypeCategory { get; set; }
        public ICollection<TblUnvCharges> TblUnvCharges { get; set; }
    }
}
