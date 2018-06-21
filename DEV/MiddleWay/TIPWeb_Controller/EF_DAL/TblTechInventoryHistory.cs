using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechInventoryHistory
    {
        public TblTechInventoryHistory()
        {
            TblTechAccessoryCharges = new HashSet<TblTechAccessoryCharges>();
            TblTechInventoryAccessories = new HashSet<TblTechInventoryAccessories>();
            TblTechInventoryDueDates = new HashSet<TblTechInventoryDueDates>();
            TblTechInventoryStatusChangeRequestsApprovedDeniedInventoryHistoryU = new HashSet<TblTechInventoryStatusChangeRequests>();
            TblTechInventoryStatusChangeRequestsInventoryHistoryU = new HashSet<TblTechInventoryStatusChangeRequests>();
            TblTechStudentInventory = new HashSet<TblTechStudentInventory>();
        }

        public int InventoryHistoryUid { get; set; }
        public int InventoryUid { get; set; }
        public int InventoryTypeUid { get; set; }
        public int SiteUid { get; set; }
        public int EntityUid { get; set; }
        public int EntityTypeUid { get; set; }
        public int StatusUid { get; set; }
        public int OriginSiteUid { get; set; }
        public int OriginStatusUid { get; set; }
        public int OriginEntityUid { get; set; }
        public int OriginEntityTypeUid { get; set; }
        public string InventoryHistoryNotes { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int? ParentInventoryUid { get; set; }
        public int? OriginParentInventoryUid { get; set; }
        public int BulkEditUid { get; set; }
        public int InventorySourceUid { get; set; }
        public int ContainerUid { get; set; }
        public int OriginContainerUid { get; set; }
        public int ArchiveUid { get; set; }
        public int OriginArchiveUid { get; set; }
        public int OperationUid { get; set; }

        public TblTechContainers ContainerU { get; set; }
        public TblUnvEntityTypes EntityTypeU { get; set; }
        public TblTechInventorySource InventorySourceU { get; set; }
        public TblTechInventoryTypes InventoryTypeU { get; set; }
        public TblTechInventory InventoryU { get; set; }
        public TblTechOperations OperationU { get; set; }
        public TblTechContainers OriginContainerU { get; set; }
        public TblStatus StatusU { get; set; }
        public ICollection<TblTechAccessoryCharges> TblTechAccessoryCharges { get; set; }
        public ICollection<TblTechInventoryAccessories> TblTechInventoryAccessories { get; set; }
        public ICollection<TblTechInventoryDueDates> TblTechInventoryDueDates { get; set; }
        public ICollection<TblTechInventoryStatusChangeRequests> TblTechInventoryStatusChangeRequestsApprovedDeniedInventoryHistoryU { get; set; }
        public ICollection<TblTechInventoryStatusChangeRequests> TblTechInventoryStatusChangeRequestsInventoryHistoryU { get; set; }
        public ICollection<TblTechStudentInventory> TblTechStudentInventory { get; set; }
    }
}
