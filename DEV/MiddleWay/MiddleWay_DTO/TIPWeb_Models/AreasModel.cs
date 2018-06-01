using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.TIPWeb_Models
{
    public class AreasModel
    {
        public int AreaID { get; set; }
        public string AreaName { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedByUser { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
