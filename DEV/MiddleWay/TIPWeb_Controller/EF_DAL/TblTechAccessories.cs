using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechAccessories
    {
        public TblTechAccessories()
        {
            TblTechAccessoryCharges = new HashSet<TblTechAccessoryCharges>();
            TblTechInventoryAccessories = new HashSet<TblTechInventoryAccessories>();
            TblTechItemAccessories = new HashSet<TblTechItemAccessories>();
        }

        public int AccessoryUid { get; set; }
        public string AccessoryName { get; set; }
        public string AccessoryDescription { get; set; }
        public decimal? AccessoryPrice { get; set; }
        public bool AccessoryConsumable { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool MissingCharge { get; set; }

        public ICollection<TblTechAccessoryCharges> TblTechAccessoryCharges { get; set; }
        public ICollection<TblTechInventoryAccessories> TblTechInventoryAccessories { get; set; }
        public ICollection<TblTechItemAccessories> TblTechItemAccessories { get; set; }
    }
}
