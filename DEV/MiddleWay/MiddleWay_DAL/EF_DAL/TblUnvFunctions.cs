using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUnvFunctions
    {
        public TblUnvFunctions()
        {
            TblUserFunctions = new HashSet<TblUserFunctions>();
            TblUserRoleFunctions = new HashSet<TblUserRoleFunctions>();
        }

        public int FunctionUid { get; set; }
        public string FunctionName { get; set; }
        public string FunctionDescription { get; set; }
        public bool Active { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public ICollection<TblUserFunctions> TblUserFunctions { get; set; }
        public ICollection<TblUserRoleFunctions> TblUserRoleFunctions { get; set; }
    }
}
