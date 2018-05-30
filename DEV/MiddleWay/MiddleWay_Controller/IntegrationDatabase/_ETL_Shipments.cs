using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class _ETL_Shipments
    {
        //[MaxLength( , ErrorMessage = "")]
        public bla _ETL_ShipmentsUid { get; set; } //INT IDENTITY(1,1) NOT NULL,
        public bla ProcessUid { get; set; } //INT NOT NULL,
        public bla PurchaseItemShipmentUid { get; set; } // INT NOT NULL,
        public bla PurchaseItemDetailUid { get; set; } //INT NOT NULL,
        public bla OrderNumber { get; set; } //VARCHAR(50) NOT NULL,
        public bla LineNumber { get; set; } //INT NOT NULL,
        public bla ShippedToSiteUid { get; set; } // INT NOT NULL,
        public bla SiteID { get; set; } //VARCHAR(100) NULL,
        public bla TicketNumber { get; set; } // INT NULL,
        public bla QuantityShipped { get; set; } //INT NOT NULL,
        public bla TicketedByUserID { get; set; } //INT NULL,
        public bla TicketedBy { get; set; } //VARCHAR(50) NULL,
        public bla TicketedDate { get; set; } //DATETIME NULL,
        public bla StatusID { get; set; } // INT NOT NULL,
        public bla Status { get; set; } //VARCHAR(50) NULL,
        public bla InvoiceNumber { get; set; } //VARCHAR(25) NULL,
        public bla InvoiceDate { get; set; } //DATE NULL
    }
}
