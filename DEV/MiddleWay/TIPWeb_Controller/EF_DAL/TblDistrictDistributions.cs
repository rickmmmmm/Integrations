using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblDistrictDistributions
    {
        public TblDistrictDistributions()
        {
            TblDistrictDistributionsTx = new HashSet<TblDistrictDistributionsTx>();
        }

        public int DistributionId { get; set; }
        public string Isbn { get; set; }
        public string Code { get; set; }
        public string Source { get; set; }
        public decimal? Amount { get; set; }
        public int? Copies { get; set; }
        public string Notes { get; set; }
        public int UserId { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<TblDistrictDistributionsTx> TblDistrictDistributionsTx { get; set; }
    }
}
