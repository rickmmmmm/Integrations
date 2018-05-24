using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblVendorOrderDetails
    {
        public int VendorOrderDetailsUid { get; set; }
        public int VendorOrderUid { get; set; }
        public int BookInventoryUid { get; set; }
        public int FundingSourceUid { get; set; }
        public int Ordered { get; set; }
        public DateTime? DateOrdered { get; set; }
        public int? Received { get; set; }
        public DateTime? DateReceived { get; set; }
        public int StatusId { get; set; }
        public decimal? Price { get; set; }
        public int UserId { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateArriving { get; set; }

        public TblBookInventory BookInventoryU { get; set; }
        public TblVendorOrders VendorOrderU { get; set; }
    }
}
