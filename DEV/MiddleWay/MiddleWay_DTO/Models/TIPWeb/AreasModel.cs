using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.TIPWeb
{
    public class AreasModel
    {
        public int AreaUid { get; set; }
        public string AreaName { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
