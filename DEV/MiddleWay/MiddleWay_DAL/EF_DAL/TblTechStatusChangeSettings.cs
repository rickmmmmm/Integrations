using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechStatusChangeSettings
    {
        public TblTechStatusChangeSettings()
        {
            TblTechStatusChangeSettingsStatus = new HashSet<TblTechStatusChangeSettingsStatus>();
        }

        public int StatusChangeSettingsUid { get; set; }
        public int RecipientUid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public bool Active { get; set; }

        public TblUnvRecipient RecipientU { get; set; }
        public ICollection<TblTechStatusChangeSettingsStatus> TblTechStatusChangeSettingsStatus { get; set; }
    }
}
