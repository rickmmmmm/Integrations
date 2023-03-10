using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblStudents
    {
        public TblStudents()
        {
            TblStudentsSchedules = new HashSet<TblStudentsSchedules>();
        }

        public string StudentId { get; set; }
        public string CampusId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Grade { get; set; }
        public string HomeRoom { get; set; }
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
        public int? Status { get; set; }
        public int StudentsUid { get; set; }
        public Guid? StudentSifguid { get; set; }
        public bool Archived { get; set; }
        public bool? New { get; set; }
        public string NotesIt { get; set; }
        public int? ActiveIt { get; set; }
        public string StudentEmail { get; set; }

        public ICollection<TblStudentsSchedules> TblStudentsSchedules { get; set; }
    }
}
