using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblRegion
    {
        public TblRegion()
        {
            TblTechSites = new HashSet<TblTechSites>();
        }

        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public string RegionDesc { get; set; }

        public ICollection<TblTechSites> TblTechSites { get; set; }
    }
}
