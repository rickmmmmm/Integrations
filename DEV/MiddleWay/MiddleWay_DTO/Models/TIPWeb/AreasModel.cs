using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.TIPWeb
{
    public class AreasModel
    {
        public int AreaUID { get; set; }
        public string AreaName { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserID { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
