using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblMobileErrors
    {
        public int RecordId { get; set; }
        public string SyncType { get; set; }
        public string Id { get; set; }
        public string Isbn { get; set; }
        public int Copies { get; set; }
        public string Accession { get; set; }
        public string CampusId { get; set; }
        public int UserId { get; set; }
        public string ErrorMsg { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
