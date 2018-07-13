using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWay_DTO.DTO_Models.TIPWeb
{
    public class PurchaseOrderDto
    {
        public string OrderNumber { get; set; }
        public int StatusId { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string SiteId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public string Notes { get; set; }
        public string Other1 { get; set; }
        public string FRN { get; set; }
        public decimal StateFunding { get; set; }
        public decimal FederalFunding { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        
        public ICollection<PurchaseItemDetailDto> PurchaseOrderDetails { get; set; }
    }

    public class PurchaseOrderFile
    {
        public string OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string VendorName { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductType { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public string FundingSource { get; set; }
        public string AccountCode { get; set; }
        public int LineNumber { get; set; }
        public string ShippedToSite { get; set; }
        public int QuantityShipped { get; set; }
        public string Notes { get; set; }
        public string Accepted { get; set; }
        public string Reason { get; set; }
    }
}
