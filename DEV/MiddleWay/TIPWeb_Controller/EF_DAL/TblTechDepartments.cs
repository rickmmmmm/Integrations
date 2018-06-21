using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechDepartments
    {
        public TblTechDepartments()
        {
            TblTechAuditDepartments = new HashSet<TblTechAuditDepartments>();
            TblTechInventory = new HashSet<TblTechInventory>();
            TblTechPurchaseItemDetails = new HashSet<TblTechPurchaseItemDetails>();
            TblTechTransferDepartmentWorkflows = new HashSet<TblTechTransferDepartmentWorkflows>();
            TblTechUserDepartments = new HashSet<TblTechUserDepartments>();
        }

        public int TechDepartmentUid { get; set; }
        public string DepartmentName { get; set; }
        public int? PrimaryContactUserId { get; set; }
        public string DepartmentId { get; set; }
        public int? TransferCompleteNotificationRecipientUid { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblUser PrimaryContactUser { get; set; }
        public TblUnvRecipient TransferCompleteNotificationRecipientU { get; set; }
        public ICollection<TblTechAuditDepartments> TblTechAuditDepartments { get; set; }
        public ICollection<TblTechInventory> TblTechInventory { get; set; }
        public ICollection<TblTechPurchaseItemDetails> TblTechPurchaseItemDetails { get; set; }
        public ICollection<TblTechTransferDepartmentWorkflows> TblTechTransferDepartmentWorkflows { get; set; }
        public ICollection<TblTechUserDepartments> TblTechUserDepartments { get; set; }
    }
}
