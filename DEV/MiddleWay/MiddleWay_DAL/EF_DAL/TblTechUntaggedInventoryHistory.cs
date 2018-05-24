using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechUntaggedInventoryHistory
    {
        public int UntaggedInventoryHistoryUid { get; set; }
        public int UntaggedInventoryUid { get; set; }
        public long Quantity { get; set; }
        public int StatusId { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public TblStatus Status { get; set; }
        public TblTechUntaggedInventory UntaggedInventoryU { get; set; }
    }
}
