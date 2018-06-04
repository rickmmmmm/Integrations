using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWay_DTO.Models
{
    public class RejectedRecord
    {
        public string OrderNumber { get; set; }
        public string RejectReason { get; set; }
        public string RejectValue { get; set; }
        public string ExceptionMessage { get; set; }
        public int LineNumber { get; set; }
    }
}
