using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechImages
    {
        public TblTechImages()
        {
            TblTechItemImages = new HashSet<TblTechItemImages>();
        }

        public int ImageUid { get; set; }
        public string ImageName { get; set; }
        public string ImageDescription { get; set; }
        public string ImageFileName { get; set; }
        public byte[] ImageContent { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public ICollection<TblTechItemImages> TblTechItemImages { get; set; }
    }
}
