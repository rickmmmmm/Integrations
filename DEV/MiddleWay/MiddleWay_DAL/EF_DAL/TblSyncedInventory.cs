﻿using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblSyncedInventory
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string CampusId { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
