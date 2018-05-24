using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class EtlInventory
    {
        public int EtlinventoryUid { get; set; }
        public string Site { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string Department { get; set; }
        public string Tag { get; set; }
        public string Serial { get; set; }
        public string FundingSource { get; set; }
        public string FundingSourceDescription { get; set; }
        public decimal? PurchasePrice { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string InventoryNotes { get; set; }
        public string PurchaseOrder { get; set; }
        public string Vendor { get; set; }
        public string AccountCode { get; set; }
        public string ParentTag { get; set; }
        public string ProductType { get; set; }
        public string Product { get; set; }
        public string ProductDescription { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string Area { get; set; }
        public string AssetId { get; set; }
        public string CustomField1 { get; set; }
        public string CustomField1Label { get; set; }
        public string CustomField2 { get; set; }
        public string CustomField2Label { get; set; }
        public string CustomField3 { get; set; }
        public string CustomField3Label { get; set; }
        public string CustomField4 { get; set; }
        public string CustomField4Label { get; set; }
        public bool ProductByNumber { get; set; }
        public int? InventoryUid { get; set; }
        public int? SiteUid { get; set; }
        public int? EntityUid { get; set; }
        public int? EntityTypeUid { get; set; }
        public int? TechDepartmentUid { get; set; }
        public int? StatusId { get; set; }
        public int? FundingSourceUid { get; set; }
        public int? PurchaseUid { get; set; }
        public int? VendorId { get; set; }
        public int? PurchaseItemDetailUid { get; set; }
        public int? PurchaseItemShipmentUid { get; set; }
        public int? PurchaseInventoryUid { get; set; }
        public int? InventorySourceUid { get; set; }
        public int? InventoryTypeUid { get; set; }
        public int? ParentInventoryUid { get; set; }
        public int? ItemUid { get; set; }
        public int? ItemTypeUid { get; set; }
        public int? InventoryMeta1Uid { get; set; }
        public int? InventoryExt1Uid { get; set; }
        public int? InventoryMeta2Uid { get; set; }
        public int? InventoryExt2Uid { get; set; }
        public int? InventoryMeta3Uid { get; set; }
        public int? InventoryExt3Uid { get; set; }
        public int? InventoryMeta4Uid { get; set; }
        public int? InventoryExt4Uid { get; set; }
        public bool Modified { get; set; }
        public bool Transferred { get; set; }
        public bool New { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
    }
}
