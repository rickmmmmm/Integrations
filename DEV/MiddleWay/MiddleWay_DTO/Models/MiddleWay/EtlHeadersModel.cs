using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay
{
    public class EtlHeadersModel
    {
        public int _ETL_HeaderUid { get; set; }
        public int ProcessTaskUid { get; set; }
        public int PurchaseUid { get; set; }
        public int OrderNumber { get; set; }
        public int StatusId { get; set; }
        public int Status { get; set; }
        public int VendorUid { get; set; }
        public int VendorName { get; set; }
        public int VendorAccountNumber { get; set; }
        public int SiteUid { get; set; }
        public int SiteId { get; set; }
        public int PurchaseDate { get; set; }
        public int EstimatedDeliveryDate { get; set; }
        public int Notes { get; set; }
        public int Other1 { get; set; }
        public int FRN { get; set; }
        public int RowId { get; set; }
        public bool Rejected { get; set; }
        public string RejectedNotes { get; set; }
    }
}
