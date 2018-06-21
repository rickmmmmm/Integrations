using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechAssetConditions
    {
        public TblTechAssetConditions()
        {
            TblTechInventoryDetails = new HashSet<TblTechInventoryDetails>();
            TblTechTagHistoryAssetConditionU = new HashSet<TblTechTagHistory>();
            TblTechTagHistoryOriginAssetConditionU = new HashSet<TblTechTagHistory>();
        }

        public int AssetConditionUid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public ICollection<TblTechInventoryDetails> TblTechInventoryDetails { get; set; }
        public ICollection<TblTechTagHistory> TblTechTagHistoryAssetConditionU { get; set; }
        public ICollection<TblTechTagHistory> TblTechTagHistoryOriginAssetConditionU { get; set; }
    }
}
