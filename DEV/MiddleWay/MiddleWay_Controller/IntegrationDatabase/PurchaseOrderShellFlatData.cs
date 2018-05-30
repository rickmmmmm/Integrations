using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class PurchaseOrderShellFlatData
    {
        //[MaxLength( , ErrorMessage = "")]
        public bla PurchaseOrderShellFlatDataUid { get; set; } // INT IDENTITY(1,1) NOT NULL,
        public bla ProcessUid { get; set; } // INT NOT NULL,
        public bla OrderNumber { get; set; } // VARCHAR(50) NOT NULL,
        public bla PurchaseDate { get; set; } // DATETIME NULL,
        public bla Status { get; set; } // VARCHAR(50) NULL,
        public bla VendorName { get; set; } // VARCHAR(100) NULL,
        public bla VendorAccountNumber { get; set; } // VARCHAR(50) NULL,
        public bla SiteID { get; set; } // VARCHAR(100) NULL,
        public bla SiteName { get; set; } // VARCHAR(100) NULL,
        public bla EstimatedDeliveryDate { get; set; } // DATETIME NULL,
        public bla Notes { get; set; } // VARCHAR(1000) NULL,
        public bla Other1 { get; set; } // VARCHAR(100) NULL,
        public bla FRN { get; set; } // VARCHAR(100)
    }
}
