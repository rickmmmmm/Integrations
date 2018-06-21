using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblBookOrdersHistoryDist
    {
        public int BookOrdersHistoryDistUid { get; set; }
        public int RequisitionUid { get; set; }
        public string Isbn { get; set; }
        public int CopiesSent { get; set; }
        public DateTime? DateCopiesSent { get; set; }
        public DateTime? TicketDate { get; set; }
        public int CopiesToShip { get; set; }
        public int CopiesReceived { get; set; }
        public int? UserId { get; set; }
        public string RandomReq { get; set; }
        public DateTime ModifiedDate { get; set; }

        public TblRequisitions RequisitionU { get; set; }
    }
}
