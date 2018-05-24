using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechUserSites
    {
        public int UserSiteUid { get; set; }
        public int UserId { get; set; }
        public int SiteUid { get; set; }

        public TblTechSites SiteU { get; set; }
        public TblUser User { get; set; }
    }
}
