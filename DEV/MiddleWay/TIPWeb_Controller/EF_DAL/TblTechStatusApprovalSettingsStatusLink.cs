using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechStatusApprovalSettingsStatusLink
    {
        public int StatusApprovalSettingStatusUid { get; set; }
        public int StatusApprovalSettingsUid { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }

        public TblTechStatusApprovalSettings StatusApprovalSettingsU { get; set; }
    }
}
