using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblVersion
    {
        public int VersionUid { get; set; }
        public int ApplicationUid { get; set; }
        public bool Active { get; set; }
        public string Version { get; set; }
        public string CombinedVersion { get; set; }
        public DateTime DateReleased { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }
        public int Revision { get; set; }
        public string Informational { get; set; }
    }
}
