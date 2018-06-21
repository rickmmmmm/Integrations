using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class ImsStudents
    {
        public int ImsStudentId { get; set; }
        public string StudentId { get; set; }
        public string CampusId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Grade { get; set; }
        public string HomeRoom { get; set; }
        public string Phone { get; set; }
        public string ParentPhone { get; set; }
        public string Email { get; set; }
        public string ParentEmail { get; set; }
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
        public int? StudentsUid { get; set; }
    }
}
