using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblAuditDetailCounts
    {
        public int AuditDetailCountUid { get; set; }
        public int AuditDetailUid { get; set; }
        public int BookInventoryUid { get; set; }
        public int Count { get; set; }
        public int? DistrictCount { get; set; }
        public int? AuditCampusOwned { get; set; }
        public string Notes { get; set; }

        public TblAuditDetails AuditDetailU { get; set; }
        public TblBookInventory BookInventoryU { get; set; }
    }
}
