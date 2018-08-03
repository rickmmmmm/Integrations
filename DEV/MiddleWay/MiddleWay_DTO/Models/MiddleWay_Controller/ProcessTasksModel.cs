using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay_Controller
{
    public class ProcessTasksModel
    {
        public int ProcessTaskUid { get; set; }
        public int ProcessUid { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Parameters { get; set; }
        public bool Successful { get; set; }
    }
}
