using System;
using System.Collections.Generic;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public partial class Transformations
    {
        public int TransformationUid { get; set; }
        public int ProcessUid { get; set; }
        public string StepName { get; set; }
        public string Function { get; set; }
        public string Parameters { get; set; }
        public string SourceColumn { get; set; }
        public string DestinationColumn { get; set; }
        public bool Enabled { get; set; }
        public int Order { get; set; }

        public Processes ProcessU { get; set; }
    }
}
