using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay
{
    public class InventoryFlatDataModel
    {
        public int InventoryFlatDataUID { get; set; }
        public int ProcessUid { get; set; }
        public int AssetID { get; set; }
        public int Tag { get; set; }
        public int Serial { get; set; }
        public int SiteID { get; set; }
        public int SiteName { get; set; }
        public int Location { get; set; }
        public int Status { get; set; }
        public int DepartmentName { get; set; }
        public int DepartmentID { get; set; }
        public int FundingSource { get; set; }
        public int FundingSourceDescription { get; set; }
        public int PurchasePrice { get; set; }
        public int PurchaseDate { get; set; }
        public int ExpirationDate { get; set; }
        public int InventoryNotes { get; set; }
        public int OrderNumber { get; set; }
        public int VendorName { get; set; }
        public int VendorAccountNumber { get; set; }
        public int ParentTag { get; set; }
        public int ProductName { get; set; }
        public int ProductDescription { get; set; }
        public int ProductByNumber { get; set; }
        public int ProductTypeName { get; set; }
        public int ProductTypeDescription { get; set; }
        public int ModelNumber { get; set; }
        public int ManufacturerName { get; set; }
        public int AreaName { get; set; }
        public int CustomField1Value { get; set; }
        public int CustomField1Label { get; set; }
        public int CustomField2Value { get; set; }
        public int CustomField2Label { get; set; }
        public int CustomField3Value { get; set; }
        public int CustomField3Label { get; set; }
        public int CustomField4Value { get; set; }
        public int CustomField4Label { get; set; }
        public int InvoiceNumber { get; set; }
        public int InvoiceDate { get; set; }
    }
}
