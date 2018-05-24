using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechInventorySource
    {
        public TblTechInventorySource()
        {
            TblTechInventory = new HashSet<TblTechInventory>();
            TblTechInventoryHistory = new HashSet<TblTechInventoryHistory>();
            TblTechUntaggedInventory = new HashSet<TblTechUntaggedInventory>();
        }

        public int InventorySourceUid { get; set; }
        public string InventorySourceName { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public ICollection<TblTechInventory> TblTechInventory { get; set; }
        public ICollection<TblTechInventoryHistory> TblTechInventoryHistory { get; set; }
        public ICollection<TblTechUntaggedInventory> TblTechUntaggedInventory { get; set; }
    }
}
