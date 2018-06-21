using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechInventoryStatusChangeRequests
    {
        public int StatusChangeRequestUid { get; set; }
        public int InventoryUid { get; set; }
        public int InventoryHistoryUid { get; set; }
        public int? ApprovedDeniedInventoryHistoryUid { get; set; }
        public bool? Approved { get; set; }
        public int? TagAttachmentUid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedByUserId { get; set; }

        public TblTechInventoryHistory ApprovedDeniedInventoryHistoryU { get; set; }
        public TblTechInventoryHistory InventoryHistoryU { get; set; }
        public TblTechInventory InventoryU { get; set; }
        public TblTechTagAttachment TagAttachmentU { get; set; }
    }
}
