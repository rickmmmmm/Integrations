using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class ReportPickTicketDetails
    {
        public int PickTicketDetailUid { get; set; }
        public int PickTicketUid { get; set; }
        public int? Ordered { get; set; }
        public int? BackOrders { get; set; }
        public int? CopiesToSend { get; set; }
        public int? CopiesSent { get; set; }
        public string Isbn { get; set; }
        public int RequisitionUid { get; set; }
        public string RandomReq { get; set; }
        public decimal? TotalValue { get; set; }
        public int? BackOrdersCalc { get; set; }
        public int? CopiesApproved { get; set; }
        public string Slc { get; set; }
        public string Title { get; set; }
        public decimal? Price { get; set; }
        public int? Publisher { get; set; }
        public string BinId { get; set; }
        public string BinDescription { get; set; }
        public string Notes { get; set; }
        public string RequisitionId { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CampusName { get; set; }
        public string CampusContact { get; set; }
        public string PhoneNumber { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingCityStateZip { get; set; }
        public string TotalCampus { get; set; }
        public string RealName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
