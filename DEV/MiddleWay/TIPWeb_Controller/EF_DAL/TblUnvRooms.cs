using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUnvRooms
    {
        public TblUnvRooms()
        {
            TblTechInventoryInstallationDetails = new HashSet<TblTechInventoryInstallationDetails>();
            TblTechStaffRooms = new HashSet<TblTechStaffRooms>();
        }

        public int RoomUid { get; set; }
        public string RoomNumber { get; set; }
        public string RoomDescription { get; set; }
        public int RoomTypeUid { get; set; }
        public string RoomOther { get; set; }
        public string RoomNotes { get; set; }
        public int SiteUid { get; set; }
        public bool Active { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblUnvRoomTypes RoomTypeU { get; set; }
        public ICollection<TblTechInventoryInstallationDetails> TblTechInventoryInstallationDetails { get; set; }
        public ICollection<TblTechStaffRooms> TblTechStaffRooms { get; set; }
    }
}
