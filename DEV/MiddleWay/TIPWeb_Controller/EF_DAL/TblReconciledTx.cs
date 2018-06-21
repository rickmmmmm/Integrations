using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblReconciledTx
    {
        public int RecondiledUid { get; set; }
        public int FkAreaUid { get; set; }
        public int FkEntityId { get; set; }
        public int FkAdjustmentId { get; set; }
        public int FkUserUid { get; set; }
        public int FkBookUid { get; set; }
        public string Accession { get; set; }
        public DateTime? DistributionDate { get; set; }
        public string Code { get; set; }
    }
}
