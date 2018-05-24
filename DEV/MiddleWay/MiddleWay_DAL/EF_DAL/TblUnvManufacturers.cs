using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUnvManufacturers
    {
        public TblUnvManufacturers()
        {
            TblTechItems = new HashSet<TblTechItems>();
        }

        public int ManufacturerUid { get; set; }
        public string ManufacturerName { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public ICollection<TblTechItems> TblTechItems { get; set; }
    }
}
