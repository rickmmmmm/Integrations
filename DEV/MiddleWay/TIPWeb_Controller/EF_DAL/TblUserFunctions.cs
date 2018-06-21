using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUserFunctions
    {
        public int UserFunctionUid { get; set; }
        public int UserId { get; set; }
        public int FunctionUid { get; set; }
        public bool Active { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblUnvFunctions FunctionU { get; set; }
        public TblUser User { get; set; }
    }
}
