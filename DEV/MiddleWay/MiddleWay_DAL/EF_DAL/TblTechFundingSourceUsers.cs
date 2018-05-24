using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechFundingSourceUsers
    {
        public int FundingSourceUserUid { get; set; }
        public int ApplicationUid { get; set; }
        public int FundingSourceUid { get; set; }
        public int UserId { get; set; }

        public TblUnvApplications ApplicationU { get; set; }
        public TblFundingSources FundingSourceU { get; set; }
        public TblUser User { get; set; }
    }
}
