using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class _ETL_Products
    {
        //[MaxLength( , ErrorMessage = "")]
        public int _ETL_ProductsUid { get; set; } //INT IDENTITY(1,1) NOT NULL,

        public int ProcessUid { get; set; } //INT NOT NULL,

        public int ProductUid { get; set; } //INT NOT NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string ProductNumber { get; set; } //VARCHAR(50) NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string ProductName { get; set; } //VARCHAR(100) NOT NULL,

        [MaxLength(1000, ErrorMessage = "")]
        public string ProductDescription { get; set; } //VARCHAR(1000) NOT NULL,

        public int ItemTypeUid { get; set; } //INT NOT NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string ProductTypeName { get; set; } //VARCHAR(50) NOT NULL,

        [MaxLength(1000, ErrorMessage = "")]
        public string ProductTypeDescription { get; set; } //VARCHAR(1000) NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string ModelNumber { get; set; } //VARCHAR(100) NULL,

        public int ManufacturerUid { get; set; } //INT NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string ManufacturerName { get; set; } //VARCHAR(100) NOT NULL,

        public decimal SuggestedPrice { get; set; } //DECIMAL NULL,

        public int AreaUid { get; set; } //INT NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string AreaName { get; set; } //VARCHAR(100) NULL,

        [MaxLength(8000, ErrorMessage = "")]
        public string ItemNotes { get; set; } //VARCHAR(8000) NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string SKU { get; set; } //VARCHAR(50) NULL,

        public bool SerialRequired { get; set; } //BIT NOT NULL,

        public int ProjectedLife { get; set; } //INT NOT NULL,

        [MaxLength(1000, ErrorMessage = "")]
        public string CustomField1 { get; set; } //VARCHAR(1000) NULL,

        [MaxLength(1000, ErrorMessage = "")]
        public string CustomField2 { get; set; } //VARCHAR(1000) NULL,

        [MaxLength(1000, ErrorMessage = "")]
        public string CustomField3 { get; set; } //VARCHAR(1000) NULL,

        public bool Active { get; set; } //BIT NOT NULL,

        public bool AllowUntagged { get; set; } //BIT NOT NULL
    }

}
