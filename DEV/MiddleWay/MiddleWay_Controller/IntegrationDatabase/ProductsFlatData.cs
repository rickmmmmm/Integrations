using System;
using System.Collections.Generic;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public partial class ProductsFlatData
    {
        public int ProductsFlatDataUid { get; set; }
        public int ProcessTaskUid { get; set; }
        public int RowId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductTypeDescription { get; set; }
        public string ModelNumber { get; set; }
        public string ManufacturerName { get; set; }
        public string SuggestedPrice { get; set; }
        public string AreaName { get; set; }
        public string Notes { get; set; }
        public string Sku { get; set; }
        public string SerialRequired { get; set; }
        public string ProjectedLife { get; set; }
        public string OtherField1 { get; set; }
        public string OtherField2 { get; set; }
        public string OtherField3 { get; set; }
        public string AllowUntagged { get; set; }
        public bool Rejected { get; set; }
        public string RejectedNotes { get; set; }

        public ProcessTasks ProcessTaskU { get; set; }
    }
}
