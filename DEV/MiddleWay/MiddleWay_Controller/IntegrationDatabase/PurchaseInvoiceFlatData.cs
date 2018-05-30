using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class PurchaseInvoiceFlatData
    {//[MaxLength(100, ErrorMessage = "")]
        public bla PurchaseInvoiceFlatDataUid { get; set; } // INT IDENTITY(1,1) NOT NULL,
        public bla ProcessUid { get; set; } // INT NOT NULL,
        public bla OrderNumber { get; set; } // VARCHAR(50) NOT NULL,
        public bla LineNumber { get; set; } // INT NULL,
        public bla InvoiceNumber { get; set; } // VARCHAR(100) NULL,
        public bla InvoiceDate { get; set; } // DATETIME NULL,
        public bla InvoiceStatus { get; set; } // VARCHAR(50) NULL,
        public bla AuthorizationStatus { get; set; } // VARCHAR(50) NULL,
        public bla AccountingDate { get; set; } // VARCHAR(50) NULL,
        public bla LineDescription { get; set; } // VARCHAR(1000) NULL,
        public bla AssetPrice { get; set; } // DECIMAL NULL,
        public bla InvoicePrice { get; set; } // DECIMAL NULL,
        public bla Quantity { get; set; } // INT NULL,
        public bla LineAmount { get; set; } // DECIMAL
    }
}
