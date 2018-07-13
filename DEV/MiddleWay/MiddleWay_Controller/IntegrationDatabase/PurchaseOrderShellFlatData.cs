using System;
using System.Collections.Generic;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public partial class PurchaseOrderShellFlatData
    {
        public int PurchaseOrderShellFlatDataUid { get; set; }
        public int ProcessUid { get; set; }
        public int RowId { get; set; }
        public string OrderNumber { get; set; }
        public string PurchaseDate { get; set; }
        public string Status { get; set; }
        public string VendorName { get; set; }
        public string VendorAccountNumber { get; set; }
        public string SiteId { get; set; }
        public string SiteName { get; set; }
        public string EstimatedDeliveryDate { get; set; }
        public string Notes { get; set; }
        public string Other1 { get; set; }
        public string Frn { get; set; }
        public bool Rejected { get; set; }
        public string RejectedNotes { get; set; }

        public Processes ProcessU { get; set; }
    }
}
