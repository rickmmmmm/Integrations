using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class ReportPickTicket
    {
        public int PickTicketUid { get; set; }
        public string PickTicketNumber { get; set; }
        public int RequisitionUid { get; set; }
        public bool FirstPickTicket { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
