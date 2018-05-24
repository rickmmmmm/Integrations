using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechUserPermissionTemplate
    {
        public int UserPermissionTemplateId { get; set; }
        public int UserId { get; set; }
        public int PermissionTemplateUid { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblTechPermissionTemplate PermissionTemplateU { get; set; }
        public TblUser User { get; set; }
    }
}
