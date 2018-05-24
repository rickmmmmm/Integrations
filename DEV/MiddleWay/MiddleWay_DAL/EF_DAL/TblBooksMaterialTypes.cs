using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblBooksMaterialTypes
    {
        public string Isbn { get; set; }
        public string MaterialTypeId { get; set; }
        public int BooksMaterialTypeId { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
