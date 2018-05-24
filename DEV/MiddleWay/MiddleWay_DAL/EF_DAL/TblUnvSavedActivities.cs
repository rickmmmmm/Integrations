using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUnvSavedActivities
    {
        public int SavedActivityUid { get; set; }
        public int EntityTypeUid { get; set; }
        public int ParameterTypeUid { get; set; }
        public string Parameter { get; set; }
        public string DisplayText { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblUnvEntityTypes EntityTypeU { get; set; }
    }
}
