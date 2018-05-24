using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblDurationIntervals
    {
        public TblDurationIntervals()
        {
            TblDigitalMaterialDetails = new HashSet<TblDigitalMaterialDetails>();
        }

        public int DurationIntervalUid { get; set; }
        public string DurationInterval { get; set; }

        public ICollection<TblDigitalMaterialDetails> TblDigitalMaterialDetails { get; set; }
    }
}
