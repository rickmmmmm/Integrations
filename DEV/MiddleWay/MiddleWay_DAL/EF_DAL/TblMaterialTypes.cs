using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblMaterialTypes
    {
        public int MaterialTypeId { get; set; }
        public string MaterialType { get; set; }
        public string MaterialTypeDescription { get; set; }
        public bool Digital { get; set; }
    }
}
