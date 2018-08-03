using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay_Controller
{
    public class ProcessTaskErrorsModel
    {
        public int ProcessTaskErrorUid { get; set; }
        public int ProcessTaskUid { get; set; }
        public int ErrorNumber { get; set; }
        public int ErrorDescription { get; set; }
        public int ErrorField { get; set; }
        public int RowId { get; set; }
        public int CreatedDate { get; set; }
        public bool ValidationError { get; set; }
    }
}
