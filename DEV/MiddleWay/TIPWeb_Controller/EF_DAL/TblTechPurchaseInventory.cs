using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechPurchaseInventory
    {
        public int PurchaseInventoryUid { get; set; }
        public int InventoryUid { get; set; }
        public int PurchaseItemShipmentUid { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblTechInventory InventoryU { get; set; }
        public TblTechPurchaseItemShipments PurchaseItemShipmentU { get; set; }
    }
}
