using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblBookOrderTransactions
    {
        public int BookOrderTransactionUid { get; set; }
        public int BookOrdersUid { get; set; }
        public int StatusId { get; set; }
        public DateTime EventDate { get; set; }
        public int Copies { get; set; }
        public int CreatedByUserId { get; set; }

        public TblBookOrders BookOrdersU { get; set; }
        public TblStatus Status { get; set; }
    }
}
