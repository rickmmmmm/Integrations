using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechSignatureReceipt
    {
        public int SignatureReceiptUid { get; set; }
        public int EntityTypeUid { get; set; }
        public int EntityUid { get; set; }
        public int InventoryHistoryUid { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
