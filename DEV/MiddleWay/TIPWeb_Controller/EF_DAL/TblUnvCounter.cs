using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUnvCounter
    {
        public int CounterUid { get; set; }
        public string Name { get; set; }
        public long Value { get; set; }
    }
}
