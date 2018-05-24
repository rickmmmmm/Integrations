using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblVendorBooks
    {
        public int VendorId { get; set; }
        public string Isbn { get; set; }
        public int? UserId { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public TblVendor Vendor { get; set; }
    }
}
