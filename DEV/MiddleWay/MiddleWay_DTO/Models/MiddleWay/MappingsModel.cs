using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay
{
    public class MappingsModel
    {
        public int MappingsUid { get; set; }
        public int ProcessUid { get; set; }
        public int SourceColumn { get; set; }
        public int DestinationColumn { get; set; }
        public int Enabled { get; set; }
    }
}
