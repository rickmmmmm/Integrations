using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay
{
    public class MappingsModel
    {
        public int MappingsUid { get; set; }
        public int ProcessUid { get; set; }
        public string SourceColumn { get; set; }
        public string DestinationColumn { get; set; }
        public bool Enabled { get; set; }
    }
}
