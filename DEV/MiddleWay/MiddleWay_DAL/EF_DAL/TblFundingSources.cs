using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblFundingSources
    {
        public TblFundingSources()
        {
            TblTechFundingSourceUsers = new HashSet<TblTechFundingSourceUsers>();
            TblTechInventory = new HashSet<TblTechInventory>();
            TblTechPurchaseItemDetails = new HashSet<TblTechPurchaseItemDetails>();
            TblVendorOrders = new HashSet<TblVendorOrders>();
        }

        public int FundingSourceUid { get; set; }
        public string FundingSource { get; set; }
        public string FundingDesc { get; set; }
        public bool? Active { get; set; }
        public int ApplicationUid { get; set; }
        public string TransferNotificationEmail { get; set; }
        public string StatusNotificationEmail { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblUnvApplications ApplicationU { get; set; }
        public ICollection<TblTechFundingSourceUsers> TblTechFundingSourceUsers { get; set; }
        public ICollection<TblTechInventory> TblTechInventory { get; set; }
        public ICollection<TblTechPurchaseItemDetails> TblTechPurchaseItemDetails { get; set; }
        public ICollection<TblVendorOrders> TblVendorOrders { get; set; }
    }
}
