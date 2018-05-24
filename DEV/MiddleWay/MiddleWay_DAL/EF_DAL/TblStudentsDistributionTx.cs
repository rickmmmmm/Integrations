using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblStudentsDistributionTx
    {
        public int TransactionId { get; set; }
        public int? DistributionId { get; set; }
        public int StudentsUid { get; set; }
        public string Isbn { get; set; }
        public string Accession { get; set; }
        public string Code { get; set; }
        public string SourceType { get; set; }
        public string Source { get; set; }
        public int? Amount { get; set; }
        public int? Copies { get; set; }
        public string Notes { get; set; }
        public int UserId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ActionTaken { get; set; }
        public bool Archived { get; set; }

        public TblStudentsDistribution Distribution { get; set; }
    }
}
