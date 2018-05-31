using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class _ETL_Headers
    {
        //[MaxLength( , ErrorMessage = "")]
        public int _ETL_HeaderUid { get; set; } //INT IDENTITY(1,1) NOT NULL,

        public int ProcessUid { get; set; } //INT NOT NULL,

        public int PurchaseUid { get; set; } //INT NOT NULL

        [Required, MaxLength(50, ErrorMessage = "")]
        public string OrderNumber { get; set; } //VARCHAR(50) NOT NULL,

        public int StatusID { get; set; } //INT NOT NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string Status { get; set; } //VARCHAR(50) NULL,

        public int VendorUid { get; set; } //INT NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string VendorName { get; set; } //VARCHAR(100) NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string VendorAccountNumber { get; set; } //VARCHAR(50) NULL,

        public int SiteUid { get; set; } //INT NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string SiteID { get; set; } //VARCHAR(100) NULL,

        public DateTime? PurchaseDate { get; set; } //DATETIME NULL,

        public DateTime? EstimatedDeliveryDate { get; set; } //DATETIME NULL,

        [MaxLength(1000, ErrorMessage = "")]
        public string Notes { get; set; } //VARCHAR(1000) NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string Other1 { get; set; } //VARCHAR(100) NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string FRN { get; set; } //VARCHAR(50)
    }
}
