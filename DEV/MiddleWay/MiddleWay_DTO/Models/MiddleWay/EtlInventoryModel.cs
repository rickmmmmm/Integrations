using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay
{
    public class EtlInventoryModel
    {
        public int _ETL_InventoryUID { get; set; }
        public int ProcessUid { get; set; }
        public int InventoryUID { get; set; }
        public string AssetID { get; set; }
        public string Tag { get; set; }
        public string Serial { get; set; }
        public int InventoryTypeUID { get; set; }
        public string InventoryTypeName { get; set; }
        public int ItemUID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductByNumber { get; set; }
        public int ItemTypeUID { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductTypeDescription { get; set; }
        public string ModelNumber { get; set; }
        public int ManufacturerUID { get; set; }
        public string ManufacturerName { get; set; }
        public int AreaUID { get; set; }
        public string AreaName { get; set; }
        public int SiteUID { get; set; }
        public string SiteID { get; set; }
        public string SiteName { get; set; }
        public int EntityUID { get; set; }
        public string EntityID { get; set; }
        public string EntityName { get; set; }
        public int EntityTypeUID { get; set; }
        public string EntityTypeName { get; set; }
        public int StatusID { get; set; }
        public string Status { get; set; }
        public int TechDepartmentUID { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentID { get; set; }
        public int FundingSourceUID { get; set; }
        public string FundingSource { get; set; }
        public string FundingSourceDescription { get; set; }
        public decimal? PurchasePrice { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string InventoryNotes { get; set; }
        public int? ParentInventoryUID { get; set; }
        public string ParentTag { get; set; }
        public int InventorySourceUID { get; set; }
        public string InventorySourceName { get; set; }
        public int PurchaseUID { get; set; }
        public string OrderNumber { get; set; }
        public int PurchaseItemDetailUID { get; set; }
        public int LineNumber { get; set; }
        public string AccountCode { get; set; }
        public int VendorUID { get; set; }
        public string VendorName { get; set; }
        public string VendorAccountNumber { get; set; }
        public int PurchaseItemShipmentUID { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public int? InventoryExt1UID { get; set; }
        public int? InventoryMeta1UID { get; set; }
        public string CustomField1Label { get; set; }
        public string CustomField1Value { get; set; }
        public int? InventoryExt2UID { get; set; }
        public int? InventoryMeta2UID { get; set; }
        public string CustomField2Label { get; set; }
        public string CustomField2Value { get; set; }
        public int? InventoryExt3UID { get; set; }
        public int? InventoryMeta3UID { get; set; }
        public string CustomField3Label { get; set; }
        public string CustomField3Value { get; set; }
        public int? InventoryExt4UID { get; set; }
        public int? InventoryMeta4UID { get; set; }
        public string CustomField4Label { get; set; }
        public string CustomField4Value { get; set; }
    }
}
