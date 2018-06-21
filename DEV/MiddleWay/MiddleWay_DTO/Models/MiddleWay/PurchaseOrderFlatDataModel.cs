using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay
{
    public class PurchaseOrderFlatDataModel
    {
        public int PurchaseOrderFlatDataUID { get; set; }
        public int ProcessUid { get; set; }
        public int OrderNumber { get; set; }
        public int PurchaseDate { get; set; }
        public int LineNumber { get; set; }
        public int Status { get; set; }
        public int VendorName { get; set; }
        public int VendorAccountNumber { get; set; }
        public int SiteID { get; set; }
        public int SiteName { get; set; }
        public int ProductName { get; set; }
        public int ProductDescription { get; set; }
        public int ProductTypeName { get; set; }
        public int ProductTypeDescription { get; set; }
        public int SiteAddedSiteID { get; set; }
        public int SiteAddedSiteName { get; set; }
        public int FundingSource { get; set; }
        public int FundingSourceDescription { get; set; }
        public int QuantityOrdered { get; set; }
        public int QuantityReceived { get; set; }
        public int PurchasePrice { get; set; }
        public int AccountCode { get; set; }
        public int DepartmentName { get; set; }
        public int DepartmentID { get; set; }
        public int CFDA { get; set; }
        public int IsAssociated { get; set; }
    }
}
