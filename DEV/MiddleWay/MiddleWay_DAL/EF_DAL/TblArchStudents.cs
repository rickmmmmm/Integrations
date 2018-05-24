using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblArchStudents
    {
        public int ArchiveStudentId { get; set; }
        public string StudentId { get; set; }
        public string CampusId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Grade { get; set; }
        public string Homeroom { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        public string Notes { get; set; }
        public string ParentEmail { get; set; }
        public int UserId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ArchiveUserId { get; set; }
        public DateTime ArchiveDate { get; set; }
        public int StudentsUid { get; set; }
    }
}
