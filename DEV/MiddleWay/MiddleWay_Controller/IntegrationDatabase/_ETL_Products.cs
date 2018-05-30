using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class _ETL_Products
    {
        //[MaxLength( , ErrorMessage = "")]
        public bla _ETL_ProductsUid { get; set; } //INT IDENTITY(1,1) NOT NULL,
        public bla ProcessUid { get; set; } //INT NOT NULL,
        public bla ProductUid { get; set; } //INT NOT NULL,
        public bla ProductNumber { get; set; } //VARCHAR(50) NOT NULL,
        public bla ProductName { get; set; } //VARCHAR(100) NOT NULL,
        public bla ProductDescription { get; set; } //VARCHAR(1000) NOT NULL,
        public bla ItemTypeUid { get; set; } //INT NOT NULL,
        public bla ProductTypeName { get; set; } //VARCHAR(50) NOT NULL,
        public bla ProductTypeDescription { get; set; } //VARCHAR(1000) NULL,
        public bla ModelNumber { get; set; } //VARCHAR(100) NULL,
        public bla ManufacturerUid { get; set; } //INT NOT NULL,
        public bla ManufacturerName { get; set; } //VARCHAR(100) NOT NULL,
        public bla SuggestedPrice { get; set; } //DECIMAL NULL,
        public bla AreaUid { get; set; } //INT NOT NULL,
        public bla AreaName { get; set; } //VARCHAR(100) NULL,
        public bla ItemNotes { get; set; } //VARCHAR(8000) NULL,
        public bla SKU { get; set; } //VARCHAR(50) NULL,
        public bla SerialRequired { get; set; } //BIT NOT NULL,
        public bla ProjectedLife { get; set; } //INT NOT NULL,
        public bla CustomField1 { get; set; } //VARCHAR(1000) NULL,
        public bla CustomField2 { get; set; } //VARCHAR(1000) NULL,
        public bla CustomField3 { get; set; } //VARCHAR(1000) NULL,
        public bla Active { get; set; } //BIT NOT NULL,
        public bla AllowUntagged { get; set; } //BIT NOT NULL
    }
}
