using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class EtlErrors
    {
        public int ErrorUid { get; set; }
        public string InterfaceMessage { get; set; }
        public string ExceptionMessage { get; set; }
        public DateTime? ErrorDate { get; set; }
        public int? ImportDataId { get; set; }
    }
}
