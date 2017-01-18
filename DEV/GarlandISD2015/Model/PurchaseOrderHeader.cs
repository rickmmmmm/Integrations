using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PurchaseOrderHeader
    {
        public int StatusUID { get; set; }
        public int VendorUID { get; set; }
        public int SiteID { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string Other1 { get; set; }
        public ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
    }
}
