using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechInventoryDetails
    {
        public int InventoryDetailUid { get; set; }
        public int InventoryUid { get; set; }
        public string PoliceReportNumber { get; set; }
        public int? AssetConditionUid { get; set; }
        public string Cfda { get; set; }
        public decimal? SalePrice { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblTechAssetConditions AssetConditionU { get; set; }
        public TblTechInventory InventoryU { get; set; }
    }
}
