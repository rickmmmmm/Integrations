using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechTransferInventory
    {
        public TblTechTransferInventory()
        {
            TblTechTransferInventoryContainerLink = new HashSet<TblTechTransferInventoryContainerLink>();
        }

        public int TransferInventoryUid { get; set; }
        public int TransferUid { get; set; }
        public int InventoryUid { get; set; }
        public bool Received { get; set; }
        public int ContainerUid { get; set; }
        public int UntaggedInventoryUid { get; set; }
        public bool? FundingSourceApproved { get; set; }
        public int OriginStatusUid { get; set; }
        public long ReceivedQuantity { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblTechContainers ContainerU { get; set; }
        public TblTechInventory InventoryU { get; set; }
        public TblStatus OriginStatusU { get; set; }
        public TblTechTransfers TransferU { get; set; }
        public TblTechUntaggedInventory UntaggedInventoryU { get; set; }
        public ICollection<TblTechTransferInventoryContainerLink> TblTechTransferInventoryContainerLink { get; set; }
    }
}
