using System;
using System.Collections.Generic;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public partial class ProcessTasks
    {
        public ProcessTasks()
        {
            ProcessTaskSteps = new HashSet<ProcessTaskSteps>();
            ProcessTasksErrors = new HashSet<ProcessTasksErrors>();
        }

        public int ProcessTaskUid { get; set; }
        public int ProcessUid { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Parameters { get; set; }
        public bool Successful { get; set; }

        public Processes ProcessU { get; set; }
        public ICollection<ProcessTaskSteps> ProcessTaskSteps { get; set; }
        public ICollection<ProcessTasksErrors> ProcessTasksErrors { get; set; }
    }
}
