using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUnvClosings
    {
        public int ClosingUid { get; set; }
        public int ClosingTypeUid { get; set; }
        public int? ClosingCampusUid { get; set; }
        public string ClosingDescription { get; set; }
        public string ClosingNote { get; set; }
        public int ClosingUserId { get; set; }
        public DateTime ClosingDate { get; set; }
        public int ApplicationUid { get; set; }

        public TblUnvApplications ApplicationU { get; set; }
        public TblUnvClosingTypes ClosingTypeU { get; set; }
        public TblUser ClosingUser { get; set; }
    }
}
