using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechTransferRequestDetails
    {
        public TblTechTransferRequestDetails()
        {
            TblTechTransferRequestDetailsHistory = new HashSet<TblTechTransferRequestDetailsHistory>();
        }

        public int TransferRequestDetailsUid { get; set; }
        public int TransferUid { get; set; }
        public int RequestedQuantity { get; set; }
        public int QuantityToFulfill { get; set; }
        public int InventoryTypeUid { get; set; }
        public int ItemUid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedByUserId { get; set; }

        public TblTechInventoryTypes InventoryTypeU { get; set; }
        public TblTechItems ItemU { get; set; }
        public TblTechTransfers TransferU { get; set; }
        public ICollection<TblTechTransferRequestDetailsHistory> TblTechTransferRequestDetailsHistory { get; set; }
    }
}
