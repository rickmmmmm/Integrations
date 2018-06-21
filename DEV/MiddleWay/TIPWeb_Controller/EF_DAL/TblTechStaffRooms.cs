using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechStaffRooms
    {
        public int StaffRoomUid { get; set; }
        public int TeacherUid { get; set; }
        public int RoomUid { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblUnvRooms RoomU { get; set; }
        public TblTeachers TeacherU { get; set; }
    }
}
