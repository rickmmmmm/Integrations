using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUnvImportTypes
    {
        public TblUnvImportTypes()
        {
            TblTechImports = new HashSet<TblTechImports>();
        }

        public int ImportTypeUid { get; set; }
        public string ImportTypeName { get; set; }
        public int ApplicationUid { get; set; }

        public ICollection<TblTechImports> TblTechImports { get; set; }
    }
}
