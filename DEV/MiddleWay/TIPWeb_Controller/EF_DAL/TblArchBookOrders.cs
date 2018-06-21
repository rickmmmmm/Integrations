using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblArchBookOrders
    {
        public int RecordId { get; set; }
        public string RequisitionId { get; set; }
        public string Isbn { get; set; }
        public string FundingSource { get; set; }
        public int? Ordered { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateReceived { get; set; }
        public int? Received { get; set; }
        public DateTime? LastModified { get; set; }
        public string CampusId { get; set; }
        public int? OrderNumber { get; set; }
        public DateTime? TicketDate { get; set; }
        public int? ToShip { get; set; }
        public DateTime? DateSent { get; set; }
        public string RandomReq { get; set; }
        public int? Batch { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public int? BackOrders { get; set; }
        public int? CopiesOnHand { get; set; }
        public int? CopiesToSend { get; set; }
        public int? CopiesSent { get; set; }
        public int? UserId { get; set; }
        public int? CopiesApproved { get; set; }
        public string Denied { get; set; }
        public bool VendorOrder { get; set; }
        public decimal? Price { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public int? Students { get; set; }
        public int? Teachers { get; set; }
        public DateTime? ArchivedDate { get; set; }
        public int? ArchivedUserId { get; set; }
        public int ArchiveId { get; set; }
    }
}
