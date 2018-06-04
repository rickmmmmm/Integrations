using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models
{
    public class PurchaseItemDetailDto
    {
        public string OrderNumber { get; set; }
        public int ItemID { get; set; }
        public int FundingSourceID { get; set; }
        public int StatusID { get; set; }
        public string SiteAddedSiteID { get; set; }
        public int QuantityOrdered { get; set; }
        public int QuantityReceived { get; set; }
        public decimal PurchasePrice { get; set; }
        public string AccountCode { get; set; }
        public int DepartmentID { get; set; }
        public int LineNumber { get; set; }
        public string CFDA { get; set; }
        public bool IsAssociated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
