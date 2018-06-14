using System;
using System.Collections.Generic;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public partial class EtlInventory
    {
        public int EtlInventoryUid { get; set; }
        public int ProcessUid { get; set; }
        public int InventoryUid { get; set; }
        public string AssetId { get; set; }
        public string Tag { get; set; }
        public string Serial { get; set; }
        public int InventoryTypeUid { get; set; }
        public string InventoryTypeName { get; set; }
        public int ItemUid { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductByNumber { get; set; }
        public int ItemTypeUid { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductTypeDescription { get; set; }
        public string ModelNumber { get; set; }
        public int? ManufacturerUid { get; set; }
        public string ManufacturerName { get; set; }
        public int? AreaUid { get; set; }
        public string AreaName { get; set; }
        public int SiteUid { get; set; }
        public string SiteId { get; set; }
        public string SiteName { get; set; }
        public int EntityUid { get; set; }
        public string EntityName { get; set; }
        public int EntityTypeUid { get; set; }
        public string EntityTypeName { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public int? TechDepartmentUid { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentId { get; set; }
        public int FundingSourceUid { get; set; }
        public string FundingSource { get; set; }
        public string FundingSourceDescription { get; set; }
        public decimal? PurchasePrice { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string InventoryNotes { get; set; }
        public int? ParentInventoryUid { get; set; }
        public string ParentTag { get; set; }
        public int InventorySourceUid { get; set; }
        public string InventorySourceName { get; set; }
        public int PurchaseUid { get; set; }
        public string OrderNumber { get; set; }
        public int PurchaseItemDetailUid { get; set; }
        public int LineNumber { get; set; }
        public string AccountCode { get; set; }
        public int VendorUid { get; set; }
        public string VendorName { get; set; }
        public string VendorAccountNumber { get; set; }
        public int PurchaseItemShipmentUid { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public int? InventoryExt1Uid { get; set; }
        public int? InventoryMeta1Uid { get; set; }
        public string CustomField1Label { get; set; }
        public string CustomField1Value { get; set; }
        public int? InventoryExt2Uid { get; set; }
        public int? InventoryMeta2Uid { get; set; }
        public string CustomField2Label { get; set; }
        public string CustomField2Value { get; set; }
        public int? InventoryExt3Uid { get; set; }
        public int? InventoryMeta3Uid { get; set; }
        public string CustomField3Label { get; set; }
        public string CustomField3Value { get; set; }
        public int? InventoryExt4Uid { get; set; }
        public int? InventoryMeta4Uid { get; set; }
        public string CustomField4Label { get; set; }
        public string CustomField4Value { get; set; }

        public Processes ProcessU { get; set; }
    }
}
