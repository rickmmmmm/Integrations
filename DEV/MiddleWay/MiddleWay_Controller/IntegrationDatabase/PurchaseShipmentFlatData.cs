using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class PurchaseShipmentFlatData
    {
        //[MaxLength( , ErrorMessage = "")]
        public bla PurchaseShipmentFlatDataUid { get; set; } // INT IDENTITY(1,1) NOT NULL,
    public bla ProcessUid { get; set; } // INT NOT NULL,
public bla OrderNumber { get; set; } // VARCHAR(50) NOT NULL,
    public bla LineNumber { get; set; } // INT NULL,
    public bla ShippedToSiteID { get; set; } // VARCHAR(100) NULL,
    public bla ShippedToSiteName { get; set; } // VARCHAR(100) NULL,
    public bla ShippedToSiteAddress { get; set; } // VARCHAR(100) NULL,
    public bla ShippedToSiteCity { get; set; } // VARCHAR(50) NULL,
    public bla ShippedToSiteState { get; set; } // VARCHAR(50) NULL,
    public bla ShippedToSiteZip { get; set; } // VARCHAR(50) NULL,
    public bla TicketNumber { get; set; } // INT NULL,
    public bla QuantityShipped { get; set; } // INT NULL,
    public bla TicketedBy { get; set; } // VARCHAR(50) NULL,
    public bla TicketedDate { get; set; } // DATETIME NULL,
    public bla Status { get; set; } // VARCHAR(50) NULL,
    public bla InvoiceNumber { get; set; } // VARCHAR(25) NULL,
    public bla InvoiceDate { get; set; } // DATETIME NULL
    }
}
