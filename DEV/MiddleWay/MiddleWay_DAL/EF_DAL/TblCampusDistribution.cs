using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblCampusDistribution
    {
        public TblCampusDistribution()
        {
            TblCampusDistributionTx = new HashSet<TblCampusDistributionTx>();
        }

        public int DistributionId { get; set; }
        public string CampusId { get; set; }
        public string Isbn { get; set; }
        public string Code { get; set; }
        public string Source { get; set; }
        public decimal? Amount { get; set; }
        public int? Copies { get; set; }
        public string Notes { get; set; }
        public int UserId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Reference { get; set; }

        public ICollection<TblCampusDistributionTx> TblCampusDistributionTx { get; set; }
    }
}
