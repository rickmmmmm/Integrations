using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.DTO_Models.TIPWeb
{
    public class PurchaseItemDetailDto
    {
        public string OrderNumber { get; set; }
        public int ItemId { get; set; }
        public int FundingSourceId { get; set; }
        public int StatusId { get; set; }
        public string SiteAddedSiteId { get; set; }
        public int QuantityOrdered { get; set; }
        public int QuantityReceived { get; set; }
        public decimal PurchasePrice { get; set; }
        public string AccountCode { get; set; }
        public int DepartmentId { get; set; }
        public int LineNumber { get; set; }
        public string CFDA { get; set; }
        public bool IsAssociated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
