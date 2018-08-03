using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay_Controller
{
    public class EtlDetailsModel
    {
        public int _ETL_DetailUid { get; set; }
        public int ProcessTaskUid { get; set; }
        public int RowId { get; set; }
        public int PurchaseItemDetailUid { get; set; }
        public int PurchaseUid { get; set; }
        public string OrderNumber { get; set; }
        public int LineNumber { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public int SiteAddedSiteUid { get; set; }
        public string SiteAddedSiteId { get; set; }
        public string SiteAddedSiteName { get; set; }
        public int FundingSourceUid { get; set; }
        public string FundingSource { get; set; }
        public string FundingSourceDescription { get; set; }
        public int ItemUid { get; set; }
        public string ProductNumber { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int ItemTypeUid { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductTypeDescription { get; set; }
        public int QuantityOrdered { get; set; }
        public int QuantityReceived { get; set; }
        public decimal? PurchasePrice { get; set; }
        public string AccountCode { get; set; }
        public int TechDepartmentUid { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentId { get; set; }
        public string CFDA { get; set; }
        public bool IsAssociated { get; set; }
        public bool Rejected { get; set; }
        public string RejectedNotes { get; set; }
    }
}
