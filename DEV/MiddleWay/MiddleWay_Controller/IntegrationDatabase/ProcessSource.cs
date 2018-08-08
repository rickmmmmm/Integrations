using System;
using System.Collections.Generic;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public partial class ProcessSource
    {
        public ProcessSource()
        {
            Processes = new HashSet<Processes>();
        }

        public int ProcessSourceUid { get; set; }
        public string ProcessSourceName { get; set; }
        public string ProcessSourceTable { get; set; }
        public string ProcessSourceDescription { get; set; }
        public bool Enabled { get; set; }

        public ICollection<Processes> Processes { get; set; }
    }
}
