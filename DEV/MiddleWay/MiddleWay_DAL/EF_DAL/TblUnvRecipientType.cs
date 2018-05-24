using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUnvRecipientType
    {
        public TblUnvRecipientType()
        {
            TblUnvRecipient = new HashSet<TblUnvRecipient>();
        }

        public int RecipientTypeUid { get; set; }
        public string RecipientTypeName { get; set; }
        public int ApplicationUid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }

        public TblUnvApplications ApplicationU { get; set; }
        public TblUser CreatedByUser { get; set; }
        public ICollection<TblUnvRecipient> TblUnvRecipient { get; set; }
    }
}
