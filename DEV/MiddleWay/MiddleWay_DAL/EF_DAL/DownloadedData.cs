using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class DownloadedData
    {
        public string FileName { get; set; }
        public string Id { get; set; }
        public string Sd { get; set; }
        public string Isbn { get; set; }
        public short? Copies { get; set; }
        public string Accession { get; set; }
        public string Message { get; set; }
    }
}
