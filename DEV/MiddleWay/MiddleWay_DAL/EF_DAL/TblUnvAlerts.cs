using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUnvAlerts
    {
        public TblUnvAlerts()
        {
            TblUnvAlertUser = new HashSet<TblUnvAlertUser>();
        }

        public int AlertUid { get; set; }
        public int ApplicationUid { get; set; }
        public int AlertTypeUid { get; set; }
        public string AlertTitle { get; set; }
        public string AlertMessage { get; set; }
        public DateTime AlertBeginDate { get; set; }
        public DateTime AlertExpirationDate { get; set; }
        public bool? Enabled { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblUnvAlertTypes AlertTypeU { get; set; }
        public TblUnvApplications ApplicationU { get; set; }
        public ICollection<TblUnvAlertUser> TblUnvAlertUser { get; set; }
    }
}
