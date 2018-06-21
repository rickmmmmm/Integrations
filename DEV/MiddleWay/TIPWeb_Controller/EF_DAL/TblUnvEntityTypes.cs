using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUnvEntityTypes
    {
        public TblUnvEntityTypes()
        {
            TblTechAuditEntityTypes = new HashSet<TblTechAuditEntityTypes>();
            TblTechContainers = new HashSet<TblTechContainers>();
            TblTechInventory = new HashSet<TblTechInventory>();
            TblTechInventoryHistory = new HashSet<TblTechInventoryHistory>();
            TblUnvSavedActivities = new HashSet<TblUnvSavedActivities>();
        }

        public int EntityTypeUid { get; set; }
        public string Description { get; set; }
        public string TableName { get; set; }
        public string IdentityColumn { get; set; }
        public int ApplicationUid { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public ICollection<TblTechAuditEntityTypes> TblTechAuditEntityTypes { get; set; }
        public ICollection<TblTechContainers> TblTechContainers { get; set; }
        public ICollection<TblTechInventory> TblTechInventory { get; set; }
        public ICollection<TblTechInventoryHistory> TblTechInventoryHistory { get; set; }
        public ICollection<TblUnvSavedActivities> TblUnvSavedActivities { get; set; }
    }
}
