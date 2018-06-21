using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblArchVendorOrders
    {
        public int RecordId { get; set; }
        public string VendorOrderId { get; set; }
        public DateTime? DateCreated { get; set; }
        public string OrderStatus { get; set; }
        public string PurchaseOrder { get; set; }
        public int? VendorId { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string SpecialInstructions { get; set; }
        public DateTime? DateSubmitted { get; set; }
        public int? UserId { get; set; }
        public string CampusId { get; set; }
        public bool DistrictCreated { get; set; }
        public DateTime? ArchivedDate { get; set; }
        public int? ArchivedUser { get; set; }
        public int ArchiveId { get; set; }
    }
}
