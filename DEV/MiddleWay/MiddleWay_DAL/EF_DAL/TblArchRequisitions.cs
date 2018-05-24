using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblArchRequisitions
    {
        public int ArchiveRequisitionId { get; set; }
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
        public int RecordId { get; set; }
        public string Notes { get; set; }
        public int ArchiveUserId { get; set; }
        public DateTime ArchiveDate { get; set; }
    }
}
