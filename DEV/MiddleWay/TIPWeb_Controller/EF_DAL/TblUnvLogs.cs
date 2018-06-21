using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUnvLogs
    {
        public int LogUid { get; set; }
        public int LogTypeUid { get; set; }
        public string LogEntry { get; set; }
        public int ApplicationUid { get; set; }
        public int CreatedByUserUid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserUid { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
