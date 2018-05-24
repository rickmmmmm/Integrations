using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUnvSupportLinks
    {
        public int SupportLinkUid { get; set; }
        public int ApplicationUid { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public string StepByStepUrl { get; set; }

        public TblUnvApplications ApplicationU { get; set; }
    }
}
