using System;
using System.Collections.Generic;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public partial class Mappings
    {
        public int MappingsUid { get; set; }
        public int ProcessUid { get; set; }
        public string SourceColumn { get; set; }
        public string DestinationColumn { get; set; }
        public bool Enabled { get; set; }

        public Processes ProcessU { get; set; }
    }
}
