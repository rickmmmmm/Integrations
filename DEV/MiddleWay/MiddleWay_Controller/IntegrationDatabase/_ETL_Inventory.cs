using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class _ETL_Inventory
    {
        //[MaxLength( , ErrorMessage = "")]
        public int _ETL_InventoryUid { get; set; } //INT IDENTITY(1,1) NOT NULL,

        public int ProcessUid { get; set; } //INT NOT NULL,

        public int InventoryUid { get; set; } //INT NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string AssetID { get; set; } //VARCHAR(100) NULL,

        [Required, MaxLength(50, ErrorMessage = "")]
        public string Tag { get; set; } //VARCHAR(50) NOT NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string Serial { get; set; } //VARCHAR(50) NULL,

        public int InventoryTypeUid { get; set; } //INT NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string InventoryTypeName { get; set; } //VARCHAR(100) NOT NULL,

        public int ItemUid { get; set; } //INT NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string ProductName { get; set; } //VARCHAR(100) NULL,

        [MaxLength(1000, ErrorMessage = "")]
        public string ProductDescription { get; set; } //VARCHAR(1000) NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string ProductByNumber { get; set; } //VARCHAR(50) NULL,

        public int ItemTypeUid { get; set; } //INT NOT NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string ProductTypeName { get; set; } //VARCHAR(50) NOT NULL,

        [MaxLength(1000, ErrorMessage = "")]
        public string ProductTypeDescription { get; set; } //VARCHAR(1000) NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string ModelNumber { get; set; } //VARCHAR(100) NULL,

        public int ManufacturerUid { get; set; } //INT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string ManufacturerName { get; set; } //VARCHAR(100) NULL,

        public int AreaUid { get; set; } //INT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string AreaName { get; set; } //VARCHAR(100) NULL,

        public int SiteUid { get; set; } //INT NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string SiteID { get; set; } //VARCHAR(100) NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string SiteName { get; set; } //VARCHAR(100) NULL,

        public int EntityUid { get; set; } //INT NOT NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string EntityName { get; set; } //VARCHAR(50) NOT NULL,

        public int EntityTypeUid { get; set; } //INT NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string EntityTypeName { get; set; } //VARCHAR(100) NOT NULL,

        public int StatusID { get; set; } //INT NOT NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string Status { get; set; } //VARCHAR(50) NULL,

        public int TechDepartmentUid { get; set; } //INT NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string DepartmentName { get; set; } //VARCHAR(50) NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string DepartmentID { get; set; } //VARCHAR(50) NULL,

        public int FundingSourceUid { get; set; } //INT NOT NULL,

        [MaxLength(500, ErrorMessage = "")]
        public string FundingSource { get; set; } //VARCHAR(500) NULL,

        [MaxLength(500, ErrorMessage = "")]
        public string FundingSourceDescription { get; set; } //VARCHAR(500) NULL,

        public decimal PurchasePrice { get; set; } //DECIMAL NULL,

        public DateTime PurchaseDate { get; set; } //DATETIME NULL,

        public DateTime ExpirationDate { get; set; } //DATETIME NULL,

        [MaxLength(3000, ErrorMessage = "")]
        public string InventoryNotes { get; set; } //VARCHAR(3000) NULL,

        public int ParentInventoryUid { get; set; } //INT NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string ParentTag { get; set; } //VARCHAR(50) NULL,

        public int InventorySourceUid { get; set; } //INT NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string InventorySourceName { get; set; } //VARCHAR(100),

        public int PurchaseUid { get; set; } //INT NOT NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string OrderNumber { get; set; } //VARCHAR(50) NOT NULL,

        public int PurchaseItemDetailUid { get; set; } //INT NOT NULL,

        public int LineNumber { get; set; } //INT NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string AccountCode { get; set; } //VARCHAR(100) NULL,

        public int VendorUid { get; set; } //INT NOT NULL,

        [MaxLength(100, ErrorMessage = "")]
        public string VendorName { get; set; } //VARCHAR(100) NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string VendorAccountNumber { get; set; } //VARCHAR(50) NULL,

        public int PurchaseItemShipmentUid { get; set; } //INT NOT NULL,

        [MaxLength(25, ErrorMessage = "")]
        public string InvoiceNumber { get; set; } //VARCHAR(25) NULL,

        public DateTime InvoiceDate { get; set; } //DATE NULL,

        public int InventoryExt1Uid { get; set; } //INT NULL,

        public int InventoryMeta1Uid { get; set; } //INT NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string CustomField1Label { get; set; } //VARCHAR(50) NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string CustomField1Value { get; set; } //VARCHAR(50) NULL,

        public int InventoryExt2Uid { get; set; } //INT NULL,

        public int InventoryMeta2Uid { get; set; } //INT NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string CustomField2Label { get; set; } //VARCHAR(50) NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string CustomField2Value { get; set; } //VARCHAR(50) NULL,

        public int InventoryExt3Uid { get; set; } //INT NULL,

        public int InventoryMeta3Uid { get; set; } //INT NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string CustomField3Label { get; set; } //VARCHAR(50) NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string CustomField3Value { get; set; } //VARCHAR(50) NULL,

        public int InventoryExt4Uid { get; set; } //INT NULL,

        public int InventoryMeta4Uid { get; set; } //INT NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string CustomField4Label { get; set; } //VARCHAR(50) NULL,

        [MaxLength(50, ErrorMessage = "")]
        public string CustomField4Value { get; set; } //VARCHAR(50) NULL
    }
}
