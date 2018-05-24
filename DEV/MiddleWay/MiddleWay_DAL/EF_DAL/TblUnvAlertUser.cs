using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUnvAlertUser
    {
        public int AlertUserUid { get; set; }
        public int AlertUid { get; set; }
        public int UserId { get; set; }

        public TblUnvAlerts AlertU { get; set; }
        public TblUser User { get; set; }
    }
}
