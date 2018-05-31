using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class PurchaseShipmentFlatData
    {
        //[MaxLength( , ErrorMessage = "")]
        public int PurchaseShipmentFlatDataUid { get; set; } // INT IDENTITY(1,1) NOT NULL,

        public int ProcessUid { get; set; } // INT NOT NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string OrderNumber { get; set; } // VARCHAR(50) NOT NULL,

        public int? LineNumber { get; set; } // INT NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string ShippedToSiteID { get; set; } // VARCHAR(100) NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string ShippedToSiteName { get; set; } // VARCHAR(100) NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string ShippedToSiteAddress { get; set; } // VARCHAR(100) NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string ShippedToSiteCity { get; set; } // VARCHAR(50) NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string ShippedToSiteState { get; set; } // VARCHAR(50) NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string ShippedToSiteZip { get; set; } // VARCHAR(50) NULL,

        public int? TicketNumber { get; set; } // INT NULL,

        public int? QuantityShipped { get; set; } // INT NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string TicketedBy { get; set; } // VARCHAR(50) NULL,

        public DateTime? TicketedDate { get; set; } // DATETIME NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string Status { get; set; } // VARCHAR(50) NULL,

        [MaxLength(25, ErrorMessage = "")]
        public string InvoiceNumber { get; set; } // VARCHAR(25) NULL,

        public DateTime? InvoiceDate { get; set; } // DATETIME NULL
    }
}
