using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay_Controller
{
    public class EtlProductsModel
    {
        public int _ETL_ProductsUid { get; set; }
        public int ProcessTaskUid { get; set; }
        public int RowId { get; set; }
        public int ItemUid { get; set; }
        public string ProductNumber { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int ItemTypeUid { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductTypeDescription { get; set; }
        public string ModelNumber { get; set; }
        public int ManufacturerUid { get; set; }
        public string ManufacturerName { get; set; }
        public decimal? SuggestedPrice { get; set; }
        public int AreaUid { get; set; }
        public string AreaName { get; set; }
        public string ItemNotes { get; set; }
        public string Sku { get; set; }
        public bool SerialRequired { get; set; }
        public int ProjectedLife { get; set; }
        public string CustomField1 { get; set; }
        public string CustomField2 { get; set; }
        public string CustomField3 { get; set; }
        public bool Active { get; set; }
        public bool AllowUntagged { get; set; }
        public bool Rejected { get; set; }
        public string RejectedNotes { get; set; }
    }
}
