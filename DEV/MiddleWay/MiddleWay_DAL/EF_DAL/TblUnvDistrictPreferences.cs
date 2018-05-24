using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUnvDistrictPreferences
    {
        public int DistrictPreferenceUid { get; set; }
        public int ApplicationUid { get; set; }
        public string DistrictPreferenceName { get; set; }
        public string DistrictPreferenceDescription { get; set; }
        public string DistrictPreferenceValue { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblUnvApplications ApplicationU { get; set; }
    }
}
