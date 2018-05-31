using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class _ETL_Details
    {
        //[MaxLength( , ErrorMessage = "")]
        public int _ETL_DetailUid { get; set; } // INT IDENTITY(1,1) NOT NULL,

        public int ProcessUid { get; set; } //INT NOT NULL,

        public int PurchaseItemDetailUid { get; set; } //INT NOT NULL,

        public int PurchaseUid { get; set; } //INT NOT NULL,

        [Required, MaxLength(50, ErrorMessage = "")]
        public string OrderNumber { get; set; } //VARCHAR(50) NOT NULL,

        public int LineNumber { get; set; } //INT NOT NULL,

        public int StatusID { get; set; } //INT NOT NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string Status { get; set; } //VARCHAR(50) NULL,

        public int SiteAddedSiteUid { get; set; } //INT NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string SiteID { get; set; } //VARCHAR(100) NULL,

        public int FundingSourceUid { get; set; } //INT NOT NULL,

        [MaxLength(500, ErrorMessage = "")]
        public string FundingSource { get; set; } //VARCHAR(500) NULL,

        [MaxLength(500, ErrorMessage = "")]
        public string FundingSourceDescription { get; set; } //VARCHAR(500) NULL,

        public int ItemUid { get; set; } //INT NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string ProductName { get; set; } //VARCHAR(100) NULL,

        [MaxLength(1000, ErrorMessage = "")]
        public string ProductDescription { get; set; } //VARCHAR(1000) NULL,

        public int ItemTypeUid { get; set; } //INT NOT NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string ProductTypeName { get; set; } //VARCHAR(50) NULL,

        [MaxLength(1000, ErrorMessage = "")]
        public string ProductTypeDescription { get; set; } //VARCHAR(1000) NULL,

        public int QuantityOrdered { get; set; } //INT NOT NULL,

        public int QuantityReceived { get; set; } //INT NOT NULL,

        public decimal PurchasePrice { get; set; } //DECIMAL NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string AccountCode { get; set; } //VARCHAR(100) NULL,

        public int? TechDepartmentUid { get; set; } //INT NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string DepartmentName { get; set; } //VARCHAR(50) NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string DepartmentID { get; set; } //VARCHAR(50) NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string CFDA { get; set; } //VARCHAR(50) NULL,

        public bool IsAssociated { get; set; } //BIT NOT NULL
    }
}
