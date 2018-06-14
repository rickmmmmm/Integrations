using System;
using System.Collections.Generic;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public partial class EtlHeaders
    {
        public int EtlHeaderUid { get; set; }
        public int ProcessUid { get; set; }
        public int PurchaseUid { get; set; }
        public string OrderNumber { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public int VendorUid { get; set; }
        public string VendorName { get; set; }
        public string VendorAccountNumber { get; set; }
        public int SiteUid { get; set; }
        public string SiteId { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public string Notes { get; set; }
        public string Other1 { get; set; }
        public string Frn { get; set; }

        public Processes ProcessU { get; set; }
    }
}
