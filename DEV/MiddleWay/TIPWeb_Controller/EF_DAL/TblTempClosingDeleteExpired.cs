using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTempClosingDeleteExpired
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Slc { get; set; }
        public int RecordId { get; set; }
        public int? UserId { get; set; }
        public string Expire { get; set; }
    }
}
