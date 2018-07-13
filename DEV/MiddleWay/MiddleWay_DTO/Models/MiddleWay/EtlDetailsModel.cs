using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay
{
    public class EtlDetailsModel
    {
        public int _ETL_DetailUid { get; set; }
        public int ProcessUid { get; set; }
        public int RowId { get; set; }
        public int PurchaseItemDetailUid { get; set; }
        public int PurchaseUid { get; set; }
        public int OrderNumber { get; set; }
        public int LineNumber { get; set; }
        public int StatusId { get; set; }
        public int Status { get; set; }
        public int SiteAddedSiteUid { get; set; }
        public int SiteId { get; set; }
        public int FundingSourceUid { get; set; }
        public int FundingSource { get; set; }
        public int FundingSourceDescription { get; set; }
        public int ItemUid { get; set; }
        public int ProductName { get; set; }
        public int ProductDescription { get; set; }
        public int ItemTypeUid { get; set; }
        public int ProductTypeName { get; set; }
        public int ProductTypeDescription { get; set; }
        public int QuantityOrdered { get; set; }
        public int QuantityReceived { get; set; }
        public int PurchasePrice { get; set; }
        public int AccountCode { get; set; }
        public int TechDepartmentUid { get; set; }
        public int DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public int CFDA { get; set; }
        public int IsAssociated { get; set; }
        public bool Rejected { get; set; }
        public string RejectedNotes { get; set; }
    }
}
