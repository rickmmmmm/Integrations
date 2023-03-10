using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class EtlActivityMonitor
    {
        public int ActivityMonitorId { get; set; }
        public string ActivityStep { get; set; }
        public string ActivityMessage { get; set; }
        public DateTime? ActivityDate { get; set; }
        public int? ImportDataId { get; set; }
    }
}
