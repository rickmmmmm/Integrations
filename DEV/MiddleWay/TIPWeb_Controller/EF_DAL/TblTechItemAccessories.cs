using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechItemAccessories
    {
        public int ItemAccessoryUid { get; set; }
        public int AccessoryUid { get; set; }
        public int ItemUid { get; set; }
        public int Quantity { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblTechAccessories AccessoryU { get; set; }
        public TblTechItems ItemU { get; set; }
    }
}
