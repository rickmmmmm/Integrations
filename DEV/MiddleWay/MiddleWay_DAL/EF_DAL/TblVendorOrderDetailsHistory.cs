﻿using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblVendorOrderDetailsHistory
    {
        public int VendorOrderDetailsHistoryUid { get; set; }
        public int VendorOrderDetailsUid { get; set; }
        public int Received { get; set; }
        public DateTime DateReceived { get; set; }
        public int UserId { get; set; }
    }
}
