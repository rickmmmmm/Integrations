using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUnvUserPreferences
    {
        public int UserPreferenceUid { get; set; }
        public int UserId { get; set; }
        public string UserPreferenceName { get; set; }
        public string UserPreferenceDescription { get; set; }
        public string UserPreferenceValue { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblUser User { get; set; }
    }
}
