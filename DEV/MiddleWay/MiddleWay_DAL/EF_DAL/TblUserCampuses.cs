using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUserCampuses
    {
        public int UserCampusUid { get; set; }
        public int UserId { get; set; }
        public int CampusUid { get; set; }

        public TblCampuses CampusU { get; set; }
        public TblUser User { get; set; }
    }
}
