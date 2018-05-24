using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblAustinIfas
    {
        public int Id { get; set; }
        public string Sc { get; set; }
        public string Cond { get; set; }
        public string Stat { get; set; }
        public string Po { get; set; }
        public string FDesc { get; set; }
        public string Mfctid { get; set; }
        public string Model { get; set; }
        public string Serialno { get; set; }
        public string Tag { get; set; }
        public string Faid { get; set; }
        public string Lctn { get; set; }
        public string Invoice { get; set; }
        public decimal? Itemamt { get; set; }
        public DateTime? Invdt { get; set; }
        public string Pc { get; set; }
        public string ExpnsAcct { get; set; }
        public decimal? Purchamt { get; set; }
        public DateTime? Inservdt { get; set; }
        public string Key { get; set; }
        public string Obj { get; set; }
        public int? ManufacturerUid { get; set; }
        public int? VendorUid { get; set; }
        public int? FundingSourceUid { get; set; }
        public int? ItemTypeUid { get; set; }
        public int? ItemUid { get; set; }
        public int? PurchaseUid { get; set; }
        public int? InventoryUid { get; set; }
        public int? SiteUid { get; set; }
        public string LongTag { get; set; }
        public int? OldIsdmetaUid { get; set; }
        public int? IsdmetaUid { get; set; }
        public int? OldFaidmetaUid { get; set; }
        public int? FaidmetaUid { get; set; }
        public int? OldAtmetaUid { get; set; }
        public int? AtmetaUid { get; set; }
        public int? PurchaseItemDetailUid { get; set; }
        public int? PurchaseItemShipmentUid { get; set; }
        public int? PurchaseInventoryUid { get; set; }
        public string TeacherId { get; set; }
        public DateTime? IssuedDate { get; set; }
        public bool Modified { get; set; }
    }
}
