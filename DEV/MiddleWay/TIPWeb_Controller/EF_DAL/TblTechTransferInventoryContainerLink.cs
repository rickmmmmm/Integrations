using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechTransferInventoryContainerLink
    {
        public int TransferInventoryContainerUid { get; set; }
        public int TransferInventoryUid { get; set; }
        public int ContainerUid { get; set; }
        public int Count { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedByUserId { get; set; }

        public TblTechContainers ContainerU { get; set; }
        public TblTechTransferInventory TransferInventoryU { get; set; }
    }
}
