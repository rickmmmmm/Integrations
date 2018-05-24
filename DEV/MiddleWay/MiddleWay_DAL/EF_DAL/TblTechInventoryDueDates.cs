using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechInventoryDueDates
    {
        public int InventoryDueDateUid { get; set; }
        public int InventoryUid { get; set; }
        public int InventoryHistoryUid { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedByUserId { get; set; }

        public TblUser CreatedByUser { get; set; }
        public TblTechInventoryHistory InventoryHistoryU { get; set; }
        public TblTechInventory InventoryU { get; set; }
        public TblUser LastModifiedByUser { get; set; }
    }
}
