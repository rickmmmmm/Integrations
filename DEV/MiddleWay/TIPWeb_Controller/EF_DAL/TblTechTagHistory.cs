using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechTagHistory
    {
        public int TagHistoryUid { get; set; }
        public int InventoryUid { get; set; }
        public string Tag { get; set; }
        public string OriginTag { get; set; }
        public string Serial { get; set; }
        public string OriginSerial { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? OriginDueDate { get; set; }
        public string ProductChangeNote { get; set; }
        public int? OriginAssetConditionUid { get; set; }
        public int? AssetConditionUid { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreateDate { get; set; }

        public TblTechAssetConditions AssetConditionU { get; set; }
        public TblUser CreatedByUser { get; set; }
        public TblTechInventory InventoryU { get; set; }
        public TblTechAssetConditions OriginAssetConditionU { get; set; }
    }
}
