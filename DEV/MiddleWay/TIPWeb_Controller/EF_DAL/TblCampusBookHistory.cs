using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblCampusBookHistory
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string CourseCode { get; set; }
        public decimal? Price { get; set; }
        public string Publisher { get; set; }
        public int? NoOrdered { get; set; }
        public int? NoReceived { get; set; }
        public int? NoDistributed { get; set; }
        public int? NoCollected { get; set; }
        public string Code { get; set; }
        public string Accession { get; set; }
    }
}
