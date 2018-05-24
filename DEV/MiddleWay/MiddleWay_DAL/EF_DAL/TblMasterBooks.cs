using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblMasterBooks
    {
        public int RecordId { get; set; }
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Slc { get; set; }
        public string Grade { get; set; }
        public string Expire { get; set; }
        public bool? Consumable { get; set; }
        public int? Publisher { get; set; }
        public decimal? Price { get; set; }
        public string Copyright { get; set; }
        public bool? Aid { get; set; }
        public bool? Conforming { get; set; }
        public string State { get; set; }
        public string MemCode { get; set; }
        public int? StudentPercent { get; set; }
        public int? TeacherPercent { get; set; }

        public TblMasterPublishers PublisherNavigation { get; set; }
    }
}
