using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblStatusTypes
    {
        public TblStatusTypes()
        {
            TblStatus = new HashSet<TblStatus>();
        }

        public int StatusTypeUid { get; set; }
        public string StatusType { get; set; }

        public ICollection<TblStatus> TblStatus { get; set; }
    }
}
