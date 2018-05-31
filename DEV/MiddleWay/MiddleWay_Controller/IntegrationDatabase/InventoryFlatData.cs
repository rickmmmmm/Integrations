using System;
using System.Collections.Generic;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public partial class InventoryFlatData
    {
        public int InventoryFlatDataUid { get; set; }

        public int ProcessUid { get; set; }

        public string AssetId { get; set; }

        public string Tag { get; set; }

        public string Serial { get; set; }

        public string SiteId { get; set; }

        public string SiteName { get; set; }

        public string Location { get; set; }

        public string Status { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentId { get; set; }

        public string FundingSource { get; set; }

        public string FundingSourceDescription { get; set; }

        public decimal? PurchasePrice { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public string InventoryNotes { get; set; }

        public string OrderNumber { get; set; }

        public string VendorName { get; set; }

        public string VendorAccountNumber { get; set; }

        public string ParentTag { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public string ProductByNumber { get; set; }

        public string ProductTypeName { get; set; }

        public string ProductTypeDescription { get; set; }

        public string ModelNumber { get; set; }

        public string ManufacturerName { get; set; }

        public string AreaName { get; set; }

        public string CustomField1Value { get; set; }

        public string CustomField1Label { get; set; }

        public string CustomField2Value { get; set; }

        public string CustomField2Label { get; set; }

        public string CustomField3Value { get; set; }

        public string CustomField3Label { get; set; }

        public string CustomField4Value { get; set; }

        public string CustomField4Label { get; set; }

        public string InvoiceNumber { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public Processes ProcessU { get; set; }
    }
}
