using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUnvRecipientInformation
    {
        public int RecipientInformationUid { get; set; }
        public int ApplicationUid { get; set; }
        public int RecipientUid { get; set; }
        public string EmailAddresses { get; set; }
        public int? UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? LastModifiedByUserId { get; set; }

        public TblUnvApplications ApplicationU { get; set; }
        public TblUser CreatedByUser { get; set; }
        public TblUser LastModifiedByUser { get; set; }
        public TblUnvRecipient RecipientU { get; set; }
        public TblUser User { get; set; }
    }
}
