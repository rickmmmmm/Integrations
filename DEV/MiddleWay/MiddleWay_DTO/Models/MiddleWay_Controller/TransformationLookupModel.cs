using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay_Controller
{
    public class TransformationLookupModel
    {
        public int TransformationLookupUid { get; set; }
        public int ProcessUid { get; set; }
        public string TransformationLookupKey { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool Enabled { get; set; }

    }
}
