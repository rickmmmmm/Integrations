using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWay_DTO.Models.TIPWeb
{
    public class PurchaseItemDetailModel
    {
        public PurchaseModel ParentPurchase { get; set; }
        public int ItemUid { get; set; }
        public int FundingSourceUid { get; set; }
        public int StatusUid { get; set; }
        public int SiteAddedSiteUid { get; set; }
        public int QuantityOrdered { get; set; }
        public int QuantityReceived { get; set; }
        public decimal PurchasePrice { get; set; }
        public string AccountCode { get; set; }
        public int TechDepartmentUid { get; set; }
        public int LineNumber { get; set; }
        public string CFDA { get; set; }
        public bool IsAssociated { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
