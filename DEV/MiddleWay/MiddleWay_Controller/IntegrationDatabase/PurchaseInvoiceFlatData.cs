using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class PurchaseInvoiceFlatData
    {//[MaxLength(100, ErrorMessage = "")]
        public int PurchaseInvoiceFlatDataUid { get; set; } // INT IDENTITY(1,1) NOT NULL,

        public int ProcessUid { get; set; } // INT NOT NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string OrderNumber { get; set; } // VARCHAR(50) NOT NULL,

        public int? LineNumber { get; set; } // INT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string InvoiceNumber { get; set; } // VARCHAR(100) NULL,

        public DateTime? InvoiceDate { get; set; } // DATETIME NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string InvoiceStatus { get; set; } // VARCHAR(50) NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string AuthorizationStatus { get; set; } // VARCHAR(50) NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string AccountingDate { get; set; } // VARCHAR(50) NULL,

        [MaxLength(1000, ErrorMessage = "")]
        public string LineDescription { get; set; } // VARCHAR(1000) NULL,

        public decimal? AssetPrice { get; set; } // DECIMAL NULL,

        public decimal? InvoicePrice { get; set; } // DECIMAL NULL,

        public int? Quantity { get; set; } // INT NULL,

        public decimal? LineAmount { get; set; } // DECIMAL
    }
}
