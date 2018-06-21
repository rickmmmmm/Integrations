using System;
using System.Collections.Generic;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public partial class TransformationLookup
    {
        public int TransformationLookupUid { get; set; }
        public int ProcessUid { get; set; }
        public string TransformationLookupKey { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool Enabled { get; set; }

        public Processes ProcessU { get; set; }
    }
}
