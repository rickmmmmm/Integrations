﻿using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class EtlImportData
    {
        public int ImportCode { get; set; }
        public DateTime? ImportDateTime { get; set; }
        public string ImportUserId { get; set; }
        public bool? ImportCompleted { get; set; }
    }
}
