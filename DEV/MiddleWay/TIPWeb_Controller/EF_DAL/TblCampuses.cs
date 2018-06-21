using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblCampuses
    {
        public TblCampuses()
        {
            TblAuditDetails = new HashSet<TblAuditDetails>();
            TblBooksCourses = new HashSet<TblBooksCourses>();
            TblCampusCoursesAssigned = new HashSet<TblCampusCoursesAssigned>();
            TblUserCampuses = new HashSet<TblUserCampuses>();
        }

        public string CampusId { get; set; }
        public string CampusName { get; set; }
        public int? Region { get; set; }
        public string CampusType { get; set; }
        public string Notes { get; set; }
        public string CampusContact { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BillAddress { get; set; }
        public string BillAddress2 { get; set; }
        public string BillCity { get; set; }
        public string BillState { get; set; }
        public string BillZip { get; set; }
        public string ShipAddress { get; set; }
        public string ShipAddress2 { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public string ShipZip { get; set; }
        public string ShipInstructions { get; set; }
        public int UserId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? Status { get; set; }
        public int CampusUid { get; set; }
        public Guid? CampusSifguid { get; set; }

        public ICollection<TblAuditDetails> TblAuditDetails { get; set; }
        public ICollection<TblBooksCourses> TblBooksCourses { get; set; }
        public ICollection<TblCampusCoursesAssigned> TblCampusCoursesAssigned { get; set; }
        public ICollection<TblUserCampuses> TblUserCampuses { get; set; }
    }
}
