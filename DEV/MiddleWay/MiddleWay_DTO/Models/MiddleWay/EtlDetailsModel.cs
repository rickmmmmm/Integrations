using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay
{
    public class EtlDetailsModel
    {
        public int _ETL_DetailUID { get; set; }
        public int ProcessUid { get; set; }
        public int PurchaseItemDetailUID { get; set; }
        public int PurchaseUID { get; set; }
        public int OrderNumber { get; set; }
        public int LineNumber { get; set; }
        public int StatusID { get; set; }
        public int Status { get; set; }
        public int SiteAddedSiteUID { get; set; }
        public int SiteID { get; set; }
        public int FundingSourceUID { get; set; }
        public int FundingSource { get; set; }
        public int FundingSourceDescription { get; set; }
        public int ItemUID { get; set; }
        public int ProductName { get; set; }
        public int ProductDescription { get; set; }
        public int ItemTypeUID { get; set; }
        public int ProductTypeName { get; set; }
        public int ProductTypeDescription { get; set; }
        public int QuantityOrdered { get; set; }
        public int QuantityReceived { get; set; }
        public int PurchasePrice { get; set; }
        public int AccountCode { get; set; }
        public int TechDepartmentUID { get; set; }
        public int DepartmentName { get; set; }
        public int DepartmentID { get; set; }
        public int CFDA { get; set; }
        public int IsAssociated { get; set; }
        public int RowID { get; set; }
        public bool Rejected { get; set; }
    }
}
