using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblBookBins
    {
        public int RecordId { get; set; }
        public string Isbn { get; set; }
        public string BinId { get; set; }
        public bool PrimaryBin { get; set; }
    }
}
