using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class EtlRejects
    {
        public int RejectUid { get; set; }
        public int? ImportCode { get; set; }
        public string Reference { get; set; }
        public string RejectReason { get; set; }
        public string RejectedValue { get; set; }
        public string ExceptionMessage { get; set; }
        public int? LineNumber { get; set; }
        public DateTime? RejectDate { get; set; }
    }
}
