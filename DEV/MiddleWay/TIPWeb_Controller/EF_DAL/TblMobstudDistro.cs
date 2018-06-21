using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblMobstudDistro
    {
        public int Uid { get; set; }
        public string StudentId { get; set; }
        public string Isbn { get; set; }
        public string Quantity { get; set; }
        public string CampusId { get; set; }
    }
}
