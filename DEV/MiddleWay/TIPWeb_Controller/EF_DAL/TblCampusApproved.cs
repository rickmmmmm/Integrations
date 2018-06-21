using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblCampusApproved
    {
        public string CampusId { get; set; }
        public string Isbn { get; set; }
        public int? StudentQuota { get; set; }
        public int? TeacherQuota { get; set; }
        public int UserId { get; set; }
        public int CampusApprovedId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
