using System;
using System.Collections.Generic;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public partial class EtlDetails
    {
        public int EtlDetailUid { get; set; }
        public int ProcessUid { get; set; }
        public int PurchaseItemDetailUid { get; set; }
        public int PurchaseUid { get; set; }
        public string OrderNumber { get; set; }
        public int LineNumber { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public int SiteAddedSiteUid { get; set; }
        public string SiteId { get; set; }
        public int FundingSourceUid { get; set; }
        public string FundingSource { get; set; }
        public string FundingSourceDescription { get; set; }
        public int ItemUid { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int ItemTypeUid { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductTypeDescription { get; set; }
        public int QuantityOrdered { get; set; }
        public int QuantityReceived { get; set; }
        public decimal PurchasePrice { get; set; }
        public string AccountCode { get; set; }
        public int? TechDepartmentUid { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentId { get; set; }
        public string Cfda { get; set; }
        public bool IsAssociated { get; set; }

        public Processes ProcessU { get; set; }
    }
}
