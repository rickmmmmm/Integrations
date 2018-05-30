using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class PurchaseOrderFlatData
    {
        //[MaxLength( , ErrorMessage = "")]
        public bla PurchaseOrderFlatDataUid { get; set; } // INT IDENTITY(1,1) NOT NULL,
        public bla ProcessUid { get; set; } // INT NOT NULL,
        public bla OrderNumber { get; set; } // VARCHAR(50) NOT NULL,
        public bla PurchaseDate { get; set; } // DATETIME NULL,
        public bla LineNumber { get; set; } // INT NULL,
        public bla Status { get; set; } // VARCHAR(50) NULL,
        public bla VendorName { get; set; } // VARCHAR(100) NULL,
        public bla VendorAccountNumber { get; set; } // VARCHAR(50) NULL,
        public bla SiteID { get; set; } // VARCHAR(100) NULL,
        public bla SiteName { get; set; } // VARCHAR(100) NULL,
        public bla ProductName { get; set; } // VARCHAR(100) NULL,
        public bla ProductDescription { get; set; } // VARCHAR(1000) NULL,
        public bla ProductTypeName { get; set; } // VARCHAR(50) NULL,
        public bla ProductTypeDescription { get; set; } // VARCHAR(1000) NULL,
        public bla SiteAddedSiteID { get; set; } // VARCHAR(100) NULL,
        public bla SiteAddedSiteName { get; set; } // VARCHAR(100) NULL,
        public bla FundingSource { get; set; } // VARCHAR(500) NULL,
        public bla FundingSourceDescription { get; set; } // VARCHAR(500) NULL,
        public bla QuantityOrdered { get; set; } // INT NULL,
        public bla QuantityReceived { get; set; } // INT NULL,
        public bla PurchasePrice { get; set; } // DECIMAL NULL,
        public bla AccountCode { get; set; } // VARCHAR(100) NULL,
        public bla DepartmentName { get; set; } // VARCHAR(50) NULL,
        public bla DepartmentID { get; set; } // VARCHAR(50) NULL,
        public bla CFDA { get; set; } // VARCHAR(50) NULL,
        public bla IsAssociated { get; set; } // BIT NULL,
}
}
