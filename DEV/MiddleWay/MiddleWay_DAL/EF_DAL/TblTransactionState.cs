using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTransactionState
    {
        public int Id { get; set; }
        public bool? State { get; set; }
        public string TransactionName { get; set; }
        public string Description { get; set; }
        public int ApplicationUid { get; set; }
    }
}
