using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechInventoryTypes
    {
        public TblTechInventoryTypes()
        {
            TblTechInventory = new HashSet<TblTechInventory>();
            TblTechInventoryHistory = new HashSet<TblTechInventoryHistory>();
            TblTechTransferRequestDetails = new HashSet<TblTechTransferRequestDetails>();
        }

        public int InventoryTypeUid { get; set; }
        public string InventoryTypeName { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public ICollection<TblTechInventory> TblTechInventory { get; set; }
        public ICollection<TblTechInventoryHistory> TblTechInventoryHistory { get; set; }
        public ICollection<TblTechTransferRequestDetails> TblTechTransferRequestDetails { get; set; }
    }
}
