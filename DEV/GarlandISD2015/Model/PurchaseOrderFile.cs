using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
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
    }
}
