using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay
{
    public class ProcessErrorsModel
    {
        public int ProcessErrorUid { get; set; }
        public int ProcessUid { get; set; }
        public int ErrorNumber { get; set; }
        public int ErrorDescription { get; set; }
        public int ErrorField { get; set; }
        public int CreatedDate { get; set; }
    }
}
