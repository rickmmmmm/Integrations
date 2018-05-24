using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUnvAreas
    {
        public TblUnvAreas()
        {
            TblTechItems = new HashSet<TblTechItems>();
        }

        public int AreaUid { get; set; }
        public string AreaName { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public ICollection<TblTechItems> TblTechItems { get; set; }
    }
}
