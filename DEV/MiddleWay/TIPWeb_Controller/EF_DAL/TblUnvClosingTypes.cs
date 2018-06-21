using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUnvClosingTypes
    {
        public TblUnvClosingTypes()
        {
            TblUnvClosings = new HashSet<TblUnvClosings>();
        }

        public int ClosingTypeUid { get; set; }
        public string ClosingType { get; set; }

        public ICollection<TblUnvClosings> TblUnvClosings { get; set; }
    }
}
