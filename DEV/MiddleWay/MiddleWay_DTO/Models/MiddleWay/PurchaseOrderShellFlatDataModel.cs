using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay
{
    public class PurchaseOrderShellFlatDataModel
    {
        public int PurchaseOrderShellFlatDataUID { get; set; }
        public int ProcessUid { get; set; }
        public int OrderNumber { get; set; }
        public int PurchaseDate { get; set; }
        public int Status { get; set; }
        public int VendorName { get; set; }
        public int VendorAccountNumber { get; set; }
        public int SiteID { get; set; }
        public int SiteName { get; set; }
        public int EstimatedDeliveryDate { get; set; }
        public int Notes { get; set; }
        public int Other1 { get; set; }
        public int FRN { get; set; }
    }
}
