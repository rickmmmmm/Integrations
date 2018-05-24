using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUnvViews
    {
        public TblUnvViews()
        {
            TblUnvUserRoles = new HashSet<TblUnvUserRoles>();
        }

        public int ViewUid { get; set; }
        public int ApplicationUid { get; set; }
        public string ViewName { get; set; }
        public string ViewDescription { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public ICollection<TblUnvUserRoles> TblUnvUserRoles { get; set; }
    }
}
