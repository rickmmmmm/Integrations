using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUnvAlertTypes
    {
        public TblUnvAlertTypes()
        {
            TblUnvAlerts = new HashSet<TblUnvAlerts>();
        }

        public int AlertTypeUid { get; set; }
        public int ApplicationUid { get; set; }
        public string AlertTypeName { get; set; }
        public string AlertTypeDescription { get; set; }

        public TblUnvApplications ApplicationU { get; set; }
        public ICollection<TblUnvAlerts> TblUnvAlerts { get; set; }
    }
}
