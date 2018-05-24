using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUnvArchives
    {
        public TblUnvArchives()
        {
            TblTechInventory = new HashSet<TblTechInventory>();
        }

        public int ArchiveUid { get; set; }
        public string ArchiveDescription { get; set; }
        public string ArchiveNotes { get; set; }
        public int ApplicationUid { get; set; }
        public DateTime ArchiveDate { get; set; }
        public int ArchiveUserId { get; set; }

        public ICollection<TblTechInventory> TblTechInventory { get; set; }
    }
}
