using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechContainers
    {
        public TblTechContainers()
        {
            TblTechInventory = new HashSet<TblTechInventory>();
            TblTechInventoryHistoryContainerU = new HashSet<TblTechInventoryHistory>();
            TblTechInventoryHistoryOriginContainerU = new HashSet<TblTechInventoryHistory>();
            TblTechTransferInventory = new HashSet<TblTechTransferInventory>();
            TblTechTransferInventoryContainerLink = new HashSet<TblTechTransferInventoryContainerLink>();
            TblTechUntaggedInventory = new HashSet<TblTechUntaggedInventory>();
        }

        public int ContainerUid { get; set; }
        public string ContainerNumber { get; set; }
        public string ContainerDescription { get; set; }
        public int ContainerTypeUid { get; set; }
        public int StatusId { get; set; }
        public int EntityUid { get; set; }
        public int EntityTypeUid { get; set; }
        public int SiteUid { get; set; }
        public decimal? ContainerWeight { get; set; }
        public decimal? ContainerValue { get; set; }
        public string ContainerNotes { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblTechContainerTypes ContainerTypeU { get; set; }
        public TblUnvEntityTypes EntityTypeU { get; set; }
        public TblStatus Status { get; set; }
        public ICollection<TblTechInventory> TblTechInventory { get; set; }
        public ICollection<TblTechInventoryHistory> TblTechInventoryHistoryContainerU { get; set; }
        public ICollection<TblTechInventoryHistory> TblTechInventoryHistoryOriginContainerU { get; set; }
        public ICollection<TblTechTransferInventory> TblTechTransferInventory { get; set; }
        public ICollection<TblTechTransferInventoryContainerLink> TblTechTransferInventoryContainerLink { get; set; }
        public ICollection<TblTechUntaggedInventory> TblTechUntaggedInventory { get; set; }
    }
}
