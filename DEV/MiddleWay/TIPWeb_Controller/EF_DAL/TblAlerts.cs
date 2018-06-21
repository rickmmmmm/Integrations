using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblAlerts
    {
        public int AlertId { get; set; }
        public int? ToUserId { get; set; }
        public string Alert { get; set; }
        public DateTime? AlertDate { get; set; }
        public bool? AlertRead { get; set; }
    }
}
