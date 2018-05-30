using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWay_DTO.Models
{
    public class PurchaseItemDetailModel
    {
        public PurchaseModel ParentPurchase { get; set; }
        public int ItemUID { get; set; }
        public int FundingSourceUID { get; set; }
        public int StatusUID { get; set; }
        public int SiteAddedSiteUID { get; set; }
        public int QuantityOrdered { get; set; }
        public int QuantityReceived { get; set; }
        public decimal PurchasePrice { get; set; }
        public string AccountCode { get; set; }
        public int TechDepartmentUID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserID { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LineNumber { get; set; }
    }
}
