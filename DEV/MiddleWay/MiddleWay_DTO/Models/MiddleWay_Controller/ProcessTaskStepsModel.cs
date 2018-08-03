using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay_Controller
{
    public class ProcessTaskStepsModel
    {
        public int ProcessTaskStepsUid { get; set; }
        public int ProcessTaskUid { get; set; }
        public string StepName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Successful { get; set; }

    }
}
