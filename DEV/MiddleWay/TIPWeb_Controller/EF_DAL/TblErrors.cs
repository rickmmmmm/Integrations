using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblErrors
    {
        public int RecordId { get; set; }
        public string Message { get; set; }
        public string PageErrorOccured { get; set; }
        public string ExceptionType { get; set; }
        public string StackTrace { get; set; }
        public string DistrictId { get; set; }
        public string UserId { get; set; }
        public string CampusId { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
