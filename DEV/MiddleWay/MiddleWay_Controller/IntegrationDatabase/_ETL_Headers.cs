using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class _ETL_Headers
    {
        //[MaxLength( , ErrorMessage = "")]
        public bla _ETL_HeaderUid { get; set; } //INT IDENTITY(1,1) NOT NULL,
        public bla ProcessUid { get; set; } //INT NOT NULL,
        public bla PurchaseUid { get; set; } //INT NOT NULL,
        public bla OrderNumber { get; set; } //VARCHAR(50) NOT NULL,
        public bla StatusID { get; set; } //INT NOT NULL,
        public bla Status { get; set; } //VARCHAR(50) NULL,
        public bla VendorUid { get; set; } //INT NOT NULL,
        public bla VendorName { get; set; } //VARCHAR(100) NULL,
        public bla VendorAccountNumber { get; set; } //VARCHAR(50) NULL,
        public bla SiteUid { get; set; } //INT NOT NULL,
        public bla SiteID { get; set; } //VARCHAR(100) NULL,
        public bla PurchaseDate { get; set; } //DATETIME NULL,
        public bla EstimatedDeliveryDate { get; set; } //DATETIME NULL,
        public bla Notes { get; set; } //VARCHAR(1000) NULL,
        public bla Other1 { get; set; } //VARCHAR(100) NULL,
        public bla FRN { get; set; } //VARCHAR(50)
    }
}
