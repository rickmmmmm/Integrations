using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblRequisitions
    {
        public TblRequisitions()
        {
            TblBookOrders = new HashSet<TblBookOrders>();
            TblBookOrdersHistoryDist = new HashSet<TblBookOrdersHistoryDist>();
            TblRequisitionMultiCampusLink = new HashSet<TblRequisitionMultiCampusLink>();
        }

        public int RequisitionUid { get; set; }
        public string RequisitionId { get; set; }
        public DateTime? DateReqApproved { get; set; }
        public string ReqStatus { get; set; }
        public bool? Approved { get; set; }
        public string VerifiedBy { get; set; }
        public DateTime? VerifiedDate { get; set; }
        public string RetrievedFromDistrict { get; set; }
        public bool? DistrictCreatedCampusReq { get; set; }
        public string CampusId { get; set; }
        public bool? OnReport { get; set; }
        public DateTime? DateSubmittedByCampus { get; set; }
        public DateTime? DateCreatedOrSent { get; set; }
        public bool? CampusReq { get; set; }
        public int? UserId { get; set; }
        public string Notes { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<TblBookOrders> TblBookOrders { get; set; }
        public ICollection<TblBookOrdersHistoryDist> TblBookOrdersHistoryDist { get; set; }
        public ICollection<TblRequisitionMultiCampusLink> TblRequisitionMultiCampusLink { get; set; }
    }
}
