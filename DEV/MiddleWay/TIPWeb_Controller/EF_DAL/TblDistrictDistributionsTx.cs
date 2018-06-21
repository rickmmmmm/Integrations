using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblDistrictDistributionsTx
    {
        public int TransactionId { get; set; }
        public int DistributionId { get; set; }
        public string Isbn { get; set; }
        public string Code { get; set; }
        public string Source { get; set; }
        public decimal? Amount { get; set; }
        public int? Copies { get; set; }
        public string Notes { get; set; }
        public int UserId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ActionTaken { get; set; }

        public TblDistrictDistributions Distribution { get; set; }
    }
}
