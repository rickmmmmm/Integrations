using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class IitInventoryImportProcess
    {
        public int InventoryImportProcessUid { get; set; }
        public int RowNumber { get; set; }
        public string ProductName { get; set; }
        public int ProductUid { get; set; }
        public string Manufacturer { get; set; }
        public int ManufacturerUid { get; set; }
        public string Model { get; set; }
        public int ProjectedLife { get; set; }
        public string Area { get; set; }
        public int AreaUid { get; set; }
        public string Sku { get; set; }
        public string Other1 { get; set; }
        public string Other2 { get; set; }
        public string Other3 { get; set; }
        public decimal? SuggestedPrice { get; set; }
        public bool SerialRequired { get; set; }
        public bool AllowUntagged { get; set; }
        public string ProductType { get; set; }
        public int ProductTypeUid { get; set; }
        public string SiteId { get; set; }
        public int SiteUid { get; set; }
        public string LocationType { get; set; }
        public int LocationTypeUid { get; set; }
        public string RoomNumber { get; set; }
        public int RoomUid { get; set; }
        public string RoomDescription { get; set; }
        public string RoomType { get; set; }
        public int RoomTypeUid { get; set; }
        public string RoomOther { get; set; }
        public int StaffOrStudentUid { get; set; }
        public string StaffOrStudentId { get; set; }
        public bool IsDuplicateStaffId { get; set; }
        public string StaffOrStudentLastName { get; set; }
        public string StaffOrStudentMiddleName { get; set; }
        public string StaffOrStudentFirstName { get; set; }
        public bool InitializeToRoom { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public int PurchaseUid { get; set; }
        public int PurchaseItemDetailUid { get; set; }
        public int PurchaseItemShipmentUid { get; set; }
        public int PurchaseInventoryUid { get; set; }
        public string Vendor { get; set; }
        public bool ReactivateVendor { get; set; }
        public int VendorUid { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string FundingSource { get; set; }
        public bool ReactivateFundingSource { get; set; }
        public int FundingSourceUid { get; set; }
        public string AccountCode { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentUid { get; set; }
        public decimal? PurchasePrice { get; set; }
        public string Tag { get; set; }
        public int InventoryUid { get; set; }
        public string SerialNumber { get; set; }
        public int InventorySourceUid { get; set; }
        public int InventoryTypeUid { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public string CustomField1 { get; set; }
        public int? InventoryMetaUid1 { get; set; }
        public int? InventoryExtUid1 { get; set; }
        public string CustomField2 { get; set; }
        public int? InventoryMetaUid2 { get; set; }
        public int? InventoryExtUid2 { get; set; }
        public string CustomField3 { get; set; }
        public int? InventoryMetaUid3 { get; set; }
        public int? InventoryExtUid3 { get; set; }
        public string CustomField4 { get; set; }
        public int? InventoryMetaUid4 { get; set; }
        public int? InventoryExtUid4 { get; set; }
    }
}
