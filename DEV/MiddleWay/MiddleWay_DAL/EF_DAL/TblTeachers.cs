using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTeachers
    {
        public TblTeachers()
        {
            TblTeachersSchedules = new HashSet<TblTeachersSchedules>();
            TblTechStaffRooms = new HashSet<TblTechStaffRooms>();
        }

        public string TeacherId { get; set; }
        public string CampusId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string HomeRoom { get; set; }
        public string Grade { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        public string Notes { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int TeachersUid { get; set; }
        public int? Status { get; set; }
        public Guid? TeacherSifguid { get; set; }
        public int StaffTypeUid { get; set; }
        public bool Archived { get; set; }
        public string NotesIt { get; set; }
        public int? ActiveIt { get; set; }
        public int? TeacherStatusUid { get; set; }

        public TblUnvStaffTypes StaffTypeU { get; set; }
        public TblTeacherStatus TeacherStatusU { get; set; }
        public ICollection<TblTeachersSchedules> TblTeachersSchedules { get; set; }
        public ICollection<TblTechStaffRooms> TblTechStaffRooms { get; set; }
    }
}
