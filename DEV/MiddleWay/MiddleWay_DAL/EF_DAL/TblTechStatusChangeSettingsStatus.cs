using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechStatusChangeSettingsStatus
    {
        public int StatusChangeSettingStatusUid { get; set; }
        public int StatusChangeSettingsUid { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }

        public TblTechStatusChangeSettings StatusChangeSettingsU { get; set; }
    }
}
