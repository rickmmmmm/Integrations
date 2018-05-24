using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblTechPermissionTemplate
    {
        public TblTechPermissionTemplate()
        {
            TblTechTemplateFunction = new HashSet<TblTechTemplateFunction>();
            TblTechUserPermissionTemplate = new HashSet<TblTechUserPermissionTemplate>();
        }

        public int PermissionTemplateUid { get; set; }
        public string TemplateName { get; set; }
        public int UserRoleUid { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblUnvUserRoles UserRoleU { get; set; }
        public ICollection<TblTechTemplateFunction> TblTechTemplateFunction { get; set; }
        public ICollection<TblTechUserPermissionTemplate> TblTechUserPermissionTemplate { get; set; }
    }
}
