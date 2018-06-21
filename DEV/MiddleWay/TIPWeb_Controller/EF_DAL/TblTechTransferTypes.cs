using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechTransferTypes
    {
        public TblTechTransferTypes()
        {
            TblTechTransfers = new HashSet<TblTechTransfers>();
        }

        public int TransferTypeUid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedByUserId { get; set; }

        public ICollection<TblTechTransfers> TblTechTransfers { get; set; }
    }
}
