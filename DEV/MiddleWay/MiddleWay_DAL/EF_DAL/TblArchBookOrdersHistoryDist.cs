using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblArchBookOrdersHistoryDist
    {
        public int RecordId { get; set; }
        public string RequisitionId { get; set; }
        public string CampusId { get; set; }
        public string Isbn { get; set; }
        public int CopiesSent { get; set; }
        public DateTime? DateCopiesSent { get; set; }
        public DateTime? TicketDate { get; set; }
        public int CopiesToShip { get; set; }
        public int CopiesReceived { get; set; }
        public int? UserId { get; set; }
        public DateTime? ArchivedDate { get; set; }
        public int? ArchivedUserId { get; set; }
        public int ArchiveId { get; set; }
    }
}
