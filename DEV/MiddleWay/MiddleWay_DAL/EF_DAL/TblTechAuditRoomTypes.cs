﻿using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechAuditRoomTypes
    {
        public int AuditRoomTypeUid { get; set; }
        public int AuditUid { get; set; }
        public int RoomTypeUid { get; set; }

        public TblUnvAudits AuditU { get; set; }
        public TblUnvRoomTypes RoomTypeU { get; set; }
    }
}
