using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class BookImages
    {
        public int BookImageUid { get; set; }
        public int BookInventoryUid { get; set; }
        public string ImageDescription { get; set; }
        public bool? DefaultImage { get; set; }
        public int? EnteredByCampus { get; set; }
        public string FileType { get; set; }
        public byte[] BookImageFile { get; set; }

        public TblBookInventory BookInventoryU { get; set; }
    }
}
