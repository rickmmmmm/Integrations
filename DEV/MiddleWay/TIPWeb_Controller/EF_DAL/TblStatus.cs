using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblStatus
    {
        public TblStatus()
        {
            TblAuditDetails = new HashSet<TblAuditDetails>();
            TblBookOrderTransactions = new HashSet<TblBookOrderTransactions>();
            TblRequisitionMultiCampus = new HashSet<TblRequisitionMultiCampus>();
            TblTechAuditDetailInventoryCountsOriginalStatus = new HashSet<TblTechAuditDetailInventoryCounts>();
            TblTechAuditDetailInventoryCountsStatus = new HashSet<TblTechAuditDetailInventoryCounts>();
            TblTechAuditDetails = new HashSet<TblTechAuditDetails>();
            TblTechContainers = new HashSet<TblTechContainers>();
            TblTechInventory = new HashSet<TblTechInventory>();
            TblTechInventoryHistory = new HashSet<TblTechInventoryHistory>();
            TblTechPurchaseItemDetails = new HashSet<TblTechPurchaseItemDetails>();
            TblTechPurchaseItemShipments = new HashSet<TblTechPurchaseItemShipments>();
            TblTechPurchases = new HashSet<TblTechPurchases>();
            TblTechTransferHistory = new HashSet<TblTechTransferHistory>();
            TblTechTransferInventory = new HashSet<TblTechTransferInventory>();
            TblTechTransferRequestNotes = new HashSet<TblTechTransferRequestNotes>();
            TblTechTransferStatusWorkflowSettings = new HashSet<TblTechTransferStatusWorkflowSettings>();
            TblTechTransfers = new HashSet<TblTechTransfers>();
            TblTechUntaggedInventoryHistory = new HashSet<TblTechUntaggedInventoryHistory>();
        }

        public int StatusId { get; set; }
        public string StatusDesc { get; set; }
        public int StatusTypeUid { get; set; }
        public int? WorkFlow { get; set; }
        public string StatusCharacter { get; set; }
        public string StatusDescription { get; set; }

        public TblStatusTypes StatusTypeU { get; set; }
        public ICollection<TblAuditDetails> TblAuditDetails { get; set; }
        public ICollection<TblBookOrderTransactions> TblBookOrderTransactions { get; set; }
        public ICollection<TblRequisitionMultiCampus> TblRequisitionMultiCampus { get; set; }
        public ICollection<TblTechAuditDetailInventoryCounts> TblTechAuditDetailInventoryCountsOriginalStatus { get; set; }
        public ICollection<TblTechAuditDetailInventoryCounts> TblTechAuditDetailInventoryCountsStatus { get; set; }
        public ICollection<TblTechAuditDetails> TblTechAuditDetails { get; set; }
        public ICollection<TblTechContainers> TblTechContainers { get; set; }
        public ICollection<TblTechInventory> TblTechInventory { get; set; }
        public ICollection<TblTechInventoryHistory> TblTechInventoryHistory { get; set; }
        public ICollection<TblTechPurchaseItemDetails> TblTechPurchaseItemDetails { get; set; }
        public ICollection<TblTechPurchaseItemShipments> TblTechPurchaseItemShipments { get; set; }
        public ICollection<TblTechPurchases> TblTechPurchases { get; set; }
        public ICollection<TblTechTransferHistory> TblTechTransferHistory { get; set; }
        public ICollection<TblTechTransferInventory> TblTechTransferInventory { get; set; }
        public ICollection<TblTechTransferRequestNotes> TblTechTransferRequestNotes { get; set; }
        public ICollection<TblTechTransferStatusWorkflowSettings> TblTechTransferStatusWorkflowSettings { get; set; }
        public ICollection<TblTechTransfers> TblTechTransfers { get; set; }
        public ICollection<TblTechUntaggedInventoryHistory> TblTechUntaggedInventoryHistory { get; set; }
    }
}
