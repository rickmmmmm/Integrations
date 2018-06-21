using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUserRoleFunctions
    {
        public int UserRoleFunctionUid { get; set; }
        public int UserRoleUid { get; set; }
        public int FunctionUid { get; set; }
        public bool Active { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblUnvFunctions FunctionU { get; set; }
        public TblUnvUserRoles UserRoleU { get; set; }
    }
}
