using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUnvUserRoles
    {
        public TblUnvUserRoles()
        {
            TblTechPermissionTemplate = new HashSet<TblTechPermissionTemplate>();
            TblUser = new HashSet<TblUser>();
            TblUserRoleFunctions = new HashSet<TblUserRoleFunctions>();
        }

        public int UserRoleUid { get; set; }
        public int ViewUid { get; set; }
        public string UserRoleName { get; set; }
        public string UserRoleDescription { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblUnvViews ViewU { get; set; }
        public ICollection<TblTechPermissionTemplate> TblTechPermissionTemplate { get; set; }
        public ICollection<TblUser> TblUser { get; set; }
        public ICollection<TblUserRoleFunctions> TblUserRoleFunctions { get; set; }
    }
}
