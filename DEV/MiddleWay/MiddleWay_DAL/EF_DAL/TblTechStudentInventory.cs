using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechStudentInventory
    {
        public int StudentInventoryUid { get; set; }
        public string StudentId { get; set; }
        public int InventoryUid { get; set; }
        public int InventoryHistoryUid { get; set; }

        public TblTechInventoryHistory InventoryHistoryU { get; set; }
        public TblTechInventory InventoryU { get; set; }
    }
}
