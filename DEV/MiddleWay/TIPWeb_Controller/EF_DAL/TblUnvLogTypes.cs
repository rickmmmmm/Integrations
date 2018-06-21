using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUnvLogTypes
    {
        public int LogTypeUid { get; set; }
        public string LogTypeName { get; set; }
        public string LogTypeDescription { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
