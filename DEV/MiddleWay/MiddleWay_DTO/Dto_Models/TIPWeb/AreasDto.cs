﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models
{
    public class AreasDto
    {
        public int AreaID { get; set; }
        public string AreaName { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedByUser { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}