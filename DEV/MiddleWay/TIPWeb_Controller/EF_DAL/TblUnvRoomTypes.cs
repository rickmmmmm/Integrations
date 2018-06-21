using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUnvRoomTypes
    {
        public TblUnvRoomTypes()
        {
            TblTechAuditRoomTypes = new HashSet<TblTechAuditRoomTypes>();
            TblUnvRooms = new HashSet<TblUnvRooms>();
        }

        public int RoomTypeUid { get; set; }
        public string RoomTypeName { get; set; }
        public string RoomTypeDescription { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public ICollection<TblTechAuditRoomTypes> TblTechAuditRoomTypes { get; set; }
        public ICollection<TblUnvRooms> TblUnvRooms { get; set; }
    }
}
