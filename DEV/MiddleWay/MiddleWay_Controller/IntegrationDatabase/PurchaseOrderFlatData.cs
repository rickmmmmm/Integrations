﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class PurchaseOrderFlatData
    {
        //[MaxLength( , ErrorMessage = "")]
        public int PurchaseOrderFlatDataUid { get; set; } // INT IDENTITY(1,1) NOT NULL,

        public int ProcessUid { get; set; } // INT NOT NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string OrderNumber { get; set; } // VARCHAR(50) NOT NULL,

        public DateTime? PurchaseDate { get; set; } // DATETIME NULL,

        public int? LineNumber { get; set; } // INT NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string Status { get; set; } // VARCHAR(50) NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string VendorName { get; set; } // VARCHAR(100) NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string VendorAccountNumber { get; set; } // VARCHAR(50) NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string SiteID { get; set; } // VARCHAR(100) NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string SiteName { get; set; } // VARCHAR(100) NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string ProductName { get; set; } // VARCHAR(100) NULL,

        [MaxLength(1000, ErrorMessage = "")]
        public string ProductDescription { get; set; } // VARCHAR(1000) NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string ProductTypeName { get; set; } // VARCHAR(50) NULL,

        [MaxLength(1000, ErrorMessage = "")]
        public string ProductTypeDescription { get; set; } // VARCHAR(1000) NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string SiteAddedSiteID { get; set; } // VARCHAR(100) NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string SiteAddedSiteName { get; set; } // VARCHAR(100) NULL,

        [MaxLength(500, ErrorMessage = "")]
        public string FundingSource { get; set; } // VARCHAR(500) NULL,

        [MaxLength(500, ErrorMessage = "")]
        public string FundingSourceDescription { get; set; } // VARCHAR(500) NULL,

        public int? QuantityOrdered { get; set; } // INT NULL,

        public int? QuantityReceived { get; set; } // INT NULL,

        public decimal? PurchasePrice { get; set; } // DECIMAL NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string AccountCode { get; set; } // VARCHAR(100) NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string DepartmentName { get; set; } // VARCHAR(50) NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string DepartmentID { get; set; } // VARCHAR(50) NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string CFDA { get; set; } // VARCHAR(50) NULL,

        public bool? IsAssociated { get; set; } // BIT NULL,
    }
}
