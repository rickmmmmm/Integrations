﻿using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblReportCategories
    {
        public int RecordId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryDescription { get; set; }
        public string CampusDistrict { get; set; }
    }
}
