using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechItemTypes
    {
        public TblTechItemTypes()
        {
            TblTechAuditItemTypes = new HashSet<TblTechAuditItemTypes>();
            TblTechInventoryMeta = new HashSet<TblTechInventoryMeta>();
            TblTechItems = new HashSet<TblTechItems>();
        }

        public int ItemTypeUid { get; set; }
        public string ItemTypeName { get; set; }
        public string ItemTypeDescription { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public ICollection<TblTechAuditItemTypes> TblTechAuditItemTypes { get; set; }
        public ICollection<TblTechInventoryMeta> TblTechInventoryMeta { get; set; }
        public ICollection<TblTechItems> TblTechItems { get; set; }
    }
}
