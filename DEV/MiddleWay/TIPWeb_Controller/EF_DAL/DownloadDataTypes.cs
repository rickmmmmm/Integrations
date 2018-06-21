using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class DownloadDataTypes
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public DateTime? DateDownloaded { get; set; }
        public string Description { get; set; }
        public string CampusId { get; set; }
    }
}
