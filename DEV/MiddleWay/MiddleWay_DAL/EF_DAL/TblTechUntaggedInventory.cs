using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechUntaggedInventory
    {
        public TblTechUntaggedInventory()
        {
            TblTechTransferInventory = new HashSet<TblTechTransferInventory>();
            TblTechUntaggedInventoryHistory = new HashSet<TblTechUntaggedInventoryHistory>();
        }

        public int UntaggedInventoryUid { get; set; }
        public int ContainerUid { get; set; }
        public int ItemUid { get; set; }
        public long Quantity { get; set; }
        public int InventorySourceUid { get; set; }
        public string Identifier { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblTechContainers ContainerU { get; set; }
        public TblTechInventorySource InventorySourceU { get; set; }
        public TblTechItems ItemU { get; set; }
        public ICollection<TblTechTransferInventory> TblTechTransferInventory { get; set; }
        public ICollection<TblTechUntaggedInventoryHistory> TblTechUntaggedInventoryHistory { get; set; }
    }
}
