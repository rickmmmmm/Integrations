using System;
using System.Collections.Generic;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public partial class EtlRawFile
    {
        public int RowId { get; set; }
        public int ProcessTaskUid { get; set; }
        public string RawData { get; set; }
        public string RawDataModified { get; set; }

        public ProcessTasks ProcessTaskU { get; set; }
    }
}
