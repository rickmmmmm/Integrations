using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblCampusInitializationTemp
    {
        public int RecordId { get; set; }
        public string CampusId { get; set; }
        public string Isbn { get; set; }
        public int? Quantity { get; set; }
    }
}
