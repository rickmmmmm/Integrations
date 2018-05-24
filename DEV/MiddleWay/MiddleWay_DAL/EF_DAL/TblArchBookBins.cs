using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblArchBookBins
    {
        public int RecordId { get; set; }
        public string Isbn { get; set; }
        public string BinId { get; set; }
        public bool PrimaryBin { get; set; }
        public DateTime? ArchivedDate { get; set; }
        public int? ArchiveduserId { get; set; }
        public int ArchiveId { get; set; }
    }
}
