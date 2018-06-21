using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay
{
    public class ProductsFlatDataModel
    {
        public int ProductsFlatDataUID { get; set; }
        public int ProcessUid { get; set; }
        public int ProductName { get; set; }
        public int ProductDescription { get; set; }
        public int ProductTypeName { get; set; }
        public int ProductTypeDescription { get; set; }
        public int ModelNumber { get; set; }
        public int ManufacturerName { get; set; }
        public int SuggestedPrice { get; set; }
        public int AreaName { get; set; }
        public int Notes { get; set; }
        public int SKU { get; set; }
        public int SerialRequired { get; set; }
        public int ProjectedLife { get; set; }
        public int OtherField1 { get; set; }
        public int OtherField2 { get; set; }
        public int OtherField3 { get; set; }
        public int AllowUntagged { get; set; }
    }
}
