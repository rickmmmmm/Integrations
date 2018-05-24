﻿using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblArchBooksMaterialTypes
    {
        public string Isbn { get; set; }
        public string MaterialTypeId { get; set; }
        public DateTime? ArchivedDate { get; set; }
        public int? ArchivedUserId { get; set; }
        public int ArchiveId { get; set; }
    }
}
