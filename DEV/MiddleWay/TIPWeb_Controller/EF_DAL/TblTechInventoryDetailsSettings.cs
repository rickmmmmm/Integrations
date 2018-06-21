using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechInventoryDetailsSettings
    {
        public int InventoryDetailSettingUid { get; set; }
        public string InventoryDetailField { get; set; }
        public bool? ShowField { get; set; }
        public bool LockField { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
