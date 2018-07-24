using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay
{
    public class EtlProductsModel
    {
        public int _ETL_ProductsUid { get; set; }
        public int ProcessTaskUid { get; set; }
        public int ProductUid { get; set; }
        public int ProductNumber { get; set; }
        public int ProductName { get; set; }
        public int ProductDescription { get; set; }
        public int ItemTypeUid { get; set; }
        public int ProductTypeName { get; set; }
        public int ProductTypeDescription { get; set; }
        public int ModelNumber { get; set; }
        public int ManufacturerUid { get; set; }
        public int ManufacturerName { get; set; }
        public int SuggestedPrice { get; set; }
        public int AreaUid { get; set; }
        public int AreaName { get; set; }
        public int ItemNotes { get; set; }
        public int SKU { get; set; }
        public int SerialRequired { get; set; }
        public int ProjectedLife { get; set; }
        public int CustomField1 { get; set; }
        public int CustomField2 { get; set; }
        public int CustomField3 { get; set; }
        public int Active { get; set; }
        public int AllowUntagged { get; set; }
        public int RowId { get; set; }
        public bool Rejected { get; set; }
        public string RejectedNotes { get; set; }
    }
}
