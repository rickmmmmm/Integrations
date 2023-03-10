using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechStatusApprovalSettings
    {
        public TblTechStatusApprovalSettings()
        {
            TblTechStatusApprovalSettingsStatusLink = new HashSet<TblTechStatusApprovalSettingsStatusLink>();
        }

        public int StatusApprovalSettingsUid { get; set; }
        public int RecipientUid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public bool Active { get; set; }

        public TblUnvRecipient RecipientU { get; set; }
        public ICollection<TblTechStatusApprovalSettingsStatusLink> TblTechStatusApprovalSettingsStatusLink { get; set; }
    }
}
