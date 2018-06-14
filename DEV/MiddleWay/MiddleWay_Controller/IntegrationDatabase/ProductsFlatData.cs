using System;
using System.Collections.Generic;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public partial class ProductsFlatData
    {
        public int ProductsFlatDataUid { get; set; }
        public int ProcessUid { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductTypeDescription { get; set; }
        public string ModelNumber { get; set; }
        public string ManufacturerName { get; set; }
        public decimal? SuggestedPrice { get; set; }
        public string AreaName { get; set; }
        public string Notes { get; set; }
        public string Sku { get; set; }
        public bool? SerialRequired { get; set; }
        public int? ProjectedLife { get; set; }
        public string OtherField1 { get; set; }
        public string OtherField2 { get; set; }
        public string OtherField3 { get; set; }
        public bool? AllowUntagged { get; set; }

        public Processes ProcessU { get; set; }
    }
}
