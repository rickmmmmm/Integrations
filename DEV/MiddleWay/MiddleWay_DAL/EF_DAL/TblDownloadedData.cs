using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblDownloadedData
    {
        public int RecordId { get; set; }
        public string FileName { get; set; }
        public string Id { get; set; }
        public string Isbn { get; set; }
        public short? Copies { get; set; }
        public string Accession { get; set; }
        public string Message { get; set; }
        public int? TypeRecordId { get; set; }
    }
}
