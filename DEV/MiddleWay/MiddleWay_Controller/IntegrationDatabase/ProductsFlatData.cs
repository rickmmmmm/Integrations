using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class ProductsFlatData
    {
        public int ProductsFlatDataUid { get; set; } // INT IDENTITY(1,1) NOT NULL,

        public int ProcessUid { get; set; } // INT NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string ProductName { get; set; } // VARCHAR(100) NULL,

        [MaxLength(1000, ErrorMessage = "")]
        public string ProductDescription { get; set; } // VARCHAR(1000) NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string ProductTypeName { get; set; } // VARCHAR(50) NULL,

        [MaxLength(1000, ErrorMessage = "")]
        public string ProductTypeDescription { get; set; } // VARCHAR(1000) NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string ModelNumber { get; set; } // VARCHAR(100) NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string ManufacturerName { get; set; } // VARCHAR(100) NULL,

        public decimal? SuggestedPrice { get; set; } // DECIMAL NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string AreaName { get; set; } // VARCHAR(100) NULL,

        [MaxLength(8000, ErrorMessage = "")]
        public string Notes { get; set; } // VARCHAR(8000) NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string SKU { get; set; } // VARCHAR(50) NULL,

        public bool? SerialRequired { get; set; } // BIT NULL,

        public int? ProjectedLife { get; set; } // INT NULL,

        [MaxLength(1000, ErrorMessage = "")]
        public string OtherField1 { get; set; } // VARCHAR(1000) NULL,

        [MaxLength(1000, ErrorMessage = "")]
        public string OtherField2 { get; set; } // VARCHAR(1000) NULL,

        [MaxLength(1000, ErrorMessage = "")]
        public string OtherField3 { get; set; } // VARCHAR(1000) NULL,

        public bool? AllowUntagged { get; set; } // BIT NULL
    }
}
