using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblVendorOrders
    {
        public TblVendorOrders()
        {
            FoundAdjustmentVendorOrders = new HashSet<FoundAdjustmentVendorOrders>();
            TblVendorOrderDetails = new HashSet<TblVendorOrderDetails>();
        }

        public int VendorOrderUid { get; set; }
        public string VendorOrderId { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? OrderStatus { get; set; }
        public string PurchaseOrder { get; set; }
        public int VendorId { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string SpecialInstructions { get; set; }
        public DateTime? DateSubmitted { get; set; }
        public int? UserId { get; set; }
        public string CampusId { get; set; }
        public bool? DistrictCreated { get; set; }
        public int? FundingSourceUid { get; set; }
        public DateTime DateModified { get; set; }

        public TblFundingSources FundingSourceU { get; set; }
        public ICollection<FoundAdjustmentVendorOrders> FoundAdjustmentVendorOrders { get; set; }
        public ICollection<TblVendorOrderDetails> TblVendorOrderDetails { get; set; }
    }
}
