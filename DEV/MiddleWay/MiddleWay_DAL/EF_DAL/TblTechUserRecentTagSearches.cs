using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechUserRecentTagSearches
    {
        public int RecentSearchUid { get; set; }
        public int SiteUid { get; set; }
        public int TagSearchUid { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
    }
}
