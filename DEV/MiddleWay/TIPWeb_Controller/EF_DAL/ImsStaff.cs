using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class ImsStaff
    {
        public int ImsStaffId { get; set; }
        public string StaffId { get; set; }
        public string CampusId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string StaffType { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        public bool Deactivate { get; set; }
        public bool? Import { get; set; }
        public bool New { get; set; }
        public string RejectMessage { get; set; }
        public int? TeachersUid { get; set; }
        public int? StaffTypeUid { get; set; }
        public int? RoomUid { get; set; }
        public string StaffStatus { get; set; }
        public string PrimarySite { get; set; }
    }
}
