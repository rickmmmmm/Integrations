using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechImports
    {
        public TblTechImports()
        {
            TblTechInventoryImports = new HashSet<TblTechInventoryImports>();
        }

        public int ImportUid { get; set; }
        public int ImportTypeUid { get; set; }
        public string ImportName { get; set; }
        public string ImportFileName { get; set; }
        public byte[] ImportFile { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public TblUnvImportTypes ImportTypeU { get; set; }
        public ICollection<TblTechInventoryImports> TblTechInventoryImports { get; set; }
    }
}
