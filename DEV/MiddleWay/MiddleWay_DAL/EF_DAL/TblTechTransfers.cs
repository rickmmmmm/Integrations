using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechTransfers
    {
        public TblTechTransfers()
        {
            TblTechTransferHistory = new HashSet<TblTechTransferHistory>();
            TblTechTransferInventory = new HashSet<TblTechTransferInventory>();
            TblTechTransferRequestDetails = new HashSet<TblTechTransferRequestDetails>();
            TblTechTransferRequestNotes = new HashSet<TblTechTransferRequestNotes>();
            TblTechTransferWorkflowHistory = new HashSet<TblTechTransferWorkflowHistory>();
            TblTechTransferWorkflows = new HashSet<TblTechTransferWorkflows>();
        }

        public int TransferUid { get; set; }
        public int TransferNumber { get; set; }
        public int SiteUid { get; set; }
        public int DestinationSiteUid { get; set; }
        public bool Complete { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int TransferTypeUid { get; set; }
        public int StatusUid { get; set; }
        public int? DriverId { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public bool Denied { get; set; }
        public string Notes { get; set; }
        public bool InEdit { get; set; }
        public int? LastEditByUserId { get; set; }
        public DateTime? LastEditDate { get; set; }
        public short? CurrentApprovalLevel { get; set; }
        public int? ReceiveApprovedByUserId { get; set; }
        public DateTime? ReceiveApprovedDate { get; set; }
        public string Phone { get; set; }
        public string TransferNotes { get; set; }
        public bool? DriverRequired { get; set; }

        public TblUser Driver { get; set; }
        public TblStatus StatusU { get; set; }
        public TblTechTransferTypes TransferTypeU { get; set; }
        public ICollection<TblTechTransferHistory> TblTechTransferHistory { get; set; }
        public ICollection<TblTechTransferInventory> TblTechTransferInventory { get; set; }
        public ICollection<TblTechTransferRequestDetails> TblTechTransferRequestDetails { get; set; }
        public ICollection<TblTechTransferRequestNotes> TblTechTransferRequestNotes { get; set; }
        public ICollection<TblTechTransferWorkflowHistory> TblTechTransferWorkflowHistory { get; set; }
        public ICollection<TblTechTransferWorkflows> TblTechTransferWorkflows { get; set; }
    }
}
