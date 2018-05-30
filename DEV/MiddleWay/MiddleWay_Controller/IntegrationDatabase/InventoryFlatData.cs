using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class InventoryFlatData
    {
        //[MaxLength( , ErrorMessage = "")]
        public bla InventoryFlatDataUid { get; set; } // INT IDENTITY(1,1) NOT NULL,
        public bla ProcessUid { get; set; } // INT NOT NULL,
        public bla AssetID { get; set; } // VARCHAR(100) NULL,
        public bla Tag { get; set; } // VARCHAR(50) NULL,
        public bla Serial { get; set; } // VARCHAR(50) NULL,
        public bla SiteID { get; set; } // VARCHAR(100) NULL,
        public bla SiteName { get; set; } // VARCHAR(100) NULL,
        public bla Location { get; set; } // VARCHAR(50) NULL,
        public bla Status { get; set; } // VARCHAR(50) NULL,
        public bla DepartmentName { get; set; } //VARCHAR(50) NULL,
        public bla DepartmentID { get; set; } //VARCHAR(50) NULL,
        public bla FundingSource { get; set; } //VARCHAR(500) NULL,
        public bla FundingSourceDescription { get; set; } //VARCHAR(500) NULL,
        public bla PurchasePrice { get; set; } //DECIMAL NULL,
        public bla PurchaseDate { get; set; } //DATETIME NULL,
        public bla ExpirationDate { get; set; } // DATETIME NULL,
        public bla InventoryNotes { get; set; } // VARCHAR(3000) NULL,
        public bla OrderNumber { get; set; } // VARCHAR(50) NOT NULL,
        public bla VendorName { get; set; } // VARCHAR(100) NULL,
        public bla VendorAccountNumber { get; set; } // VARCHAR(50) NULL,
        public bla ParentTag { get; set; } //VARCHAR(50) NULL,
        public bla ProductName { get; set; } // VARCHAR(100) NULL,
        public bla ProductDescription { get; set; } // VARCHAR(1000) NULL,
        public bla ProductByNumber { get; set; } //VARCHAR(50) NULL,
        public bla ProductTypeName { get; set; } // VARCHAR(50) NULL,
        public bla ProductTypeDescription { get; set; } // VARCHAR(1000) NULL,
        public bla ModelNumber { get; set; } // VARCHAR(100) NULL,
        public bla ManufacturerName { get; set; } // VARCHAR(100) NULL,
        public bla AreaName { get; set; } // VARCHAR(100) NULL,
        public bla CustomField1Value { get; set; } // VARCHAR(50) NULL,
        public bla CustomField1Label { get; set; } // VARCHAR(50) NULL,
        public bla CustomField2Value { get; set; } // VARCHAR(50) NULL,
        public bla CustomField2Label { get; set; } // VARCHAR(50) NULL,
        public bla CustomField3Value { get; set; } // VARCHAR(50) NULL,
        public bla CustomField3Label { get; set; } // VARCHAR(50) NULL,
        public bla CustomField4Value { get; set; } // VARCHAR(50) NULL,
        public bla CustomField4Label { get; set; } // VARCHAR(50) NULL,
        public bla InvoiceNumber { get; set; } // VARCHAR(25) NULL,
        public bla InvoiceDate { get; set; } //DATE NULL
    }
}
