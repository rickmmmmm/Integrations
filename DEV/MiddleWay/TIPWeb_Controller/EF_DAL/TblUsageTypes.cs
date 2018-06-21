using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUsageTypes
    {
        public TblUsageTypes()
        {
            TblDigitalMaterialDetails = new HashSet<TblDigitalMaterialDetails>();
        }

        public int UsageTypeUid { get; set; }
        public string UsageType { get; set; }

        public ICollection<TblDigitalMaterialDetails> TblDigitalMaterialDetails { get; set; }
    }
}
