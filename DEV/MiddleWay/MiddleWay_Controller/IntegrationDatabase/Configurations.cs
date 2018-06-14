using System;
using System.Collections.Generic;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public partial class Configurations
    {
        public int ConfigurationUid { get; set; }
        public int ProcessUid { get; set; }
        public string ConfigurationName { get; set; }
        public string ConfigurationValue { get; set; }
        public bool Enabled { get; set; }

        public Processes ProcessU { get; set; }
    }
}
