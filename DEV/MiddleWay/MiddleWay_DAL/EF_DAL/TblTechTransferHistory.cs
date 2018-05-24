using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechTransferHistory
    {
        public int TransferHistoryUid { get; set; }
        public int TransferUid { get; set; }
        public int StatusUid { get; set; }
        public int OriginStatusUid { get; set; }
        public int? DriverId { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public bool Denied { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int? SignatureUid { get; set; }

        public TblUser Driver { get; set; }
        public TblUnvSignature SignatureU { get; set; }
        public TblStatus StatusU { get; set; }
        public TblTechTransfers TransferU { get; set; }
    }
}
