using System;
using System.Collections.Generic;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public partial class ProcessErrors
    {
        public int ProcessErrorUid { get; set; }
        public int ProcessTaskUid { get; set; }
        public int? ErrorNumber { get; set; }
        public string ErrorDescription { get; set; }
        public string ErrorField { get; set; }
        public DateTime CreatedDate { get; set; }

        public ProcessTasks ProcessTaskU { get; set; }
    }
}
