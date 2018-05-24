using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechItemImages
    {
        public int ItemImageUid { get; set; }
        public int ImageUid { get; set; }
        public int ItemUid { get; set; }
        public bool? IsPrimary { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblTechImages ImageU { get; set; }
        public TblTechItems ItemU { get; set; }
    }
}
