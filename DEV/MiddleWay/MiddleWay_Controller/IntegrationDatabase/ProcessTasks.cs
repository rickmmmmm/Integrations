using System;
using System.Collections.Generic;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public partial class ProcessTasks
    {
        public ProcessTasks()
        {
            ProcessErrors = new HashSet<ProcessErrors>();
        }

        public int ProcessTaskUid { get; set; }
        public int ProcessUid { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Parameters { get; set; }

        public Processes ProcessU { get; set; }
        public ICollection<ProcessErrors> ProcessErrors { get; set; }
    }
}
