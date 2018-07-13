using System;
using System.Collections.Generic;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public partial class ProcessTaskSteps
    {
        public int ProcessTaskStepsUid { get; set; }
        public int ProcessTaskUid { get; set; }
        public string StepName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Successful { get; set; }

        public ProcessTasks ProcessTaskU { get; set; }
    }
}
