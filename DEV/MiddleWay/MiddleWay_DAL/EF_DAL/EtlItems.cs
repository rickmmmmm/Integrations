using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class EtlItems
    {
        public int EtlitemUid { get; set; }
        public string Product { get; set; }
        public string ProductNumber { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public decimal? SuggestedPrice { get; set; }
        public string Area { get; set; }
        public string ProductNotes { get; set; }
        public string Sku { get; set; }
        public int? ProjectedLife { get; set; }
        public string OtherField1 { get; set; }
        public string OtherField2 { get; set; }
        public string OtherField3 { get; set; }
        public string ProductType { get; set; }
        public string ProductTypeDescription { get; set; }
        public int? ItemUid { get; set; }
        public int? ManufacturerUid { get; set; }
        public int? AreaUid { get; set; }
        public int? ItemTypeUid { get; set; }
        public bool New { get; set; }
    }
}
