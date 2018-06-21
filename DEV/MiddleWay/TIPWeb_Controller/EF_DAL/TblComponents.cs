using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblComponents
    {
        public int RecordId { get; set; }
        public string MasterIsbn { get; set; }
        public string ComponentIsbn { get; set; }
        public string Title { get; set; }
        public decimal? Price { get; set; }
        public int? Units { get; set; }
        public string Bin { get; set; }
        public string BinAlt1 { get; set; }
        public string BinAlt2 { get; set; }
        public string BinAlt3 { get; set; }
        public string BinAlt4 { get; set; }
    }
}
