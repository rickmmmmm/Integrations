using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblDownloadDataTypes
    {
        public int RecordId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string CampusId { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
