using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWay_DTO.Models
{
    public class RejectedRecord
    {
        public string orderNumber { get; set; }
        public string rejectReason { get; set; }
        public string rejectValue { get; set; }
        public string exceptionMessage { get; set; }
        public int LineNumber { get; set; }
    }
}
