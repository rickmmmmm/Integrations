using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechSiteTypes
    {
        public TblTechSiteTypes()
        {
            TblTechSites = new HashSet<TblTechSites>();
        }

        public int SiteTypeUid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedByUserId { get; set; }

        public ICollection<TblTechSites> TblTechSites { get; set; }
    }
}
