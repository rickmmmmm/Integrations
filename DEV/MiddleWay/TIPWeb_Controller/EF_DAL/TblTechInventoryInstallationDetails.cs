using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechInventoryInstallationDetails
    {
        public int InstallationDetailUid { get; set; }
        public int InventoryUid { get; set; }
        public int SiteUid { get; set; }
        public int RoomUid { get; set; }
        public DateTime InstallationDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public TblTechInventory InventoryU { get; set; }
        public TblUnvRooms RoomU { get; set; }
        public TblTechSites SiteU { get; set; }
    }
}
