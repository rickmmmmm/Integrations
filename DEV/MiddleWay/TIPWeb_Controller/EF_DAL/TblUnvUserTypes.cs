using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUnvUserTypes
    {
        public TblUnvUserTypes()
        {
            TblTechUserTypeWorkflows = new HashSet<TblTechUserTypeWorkflows>();
            TblUser = new HashSet<TblUser>();
        }

        public int UserTypeUid { get; set; }
        public string UserTypeName { get; set; }
        public bool Active { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public ICollection<TblTechUserTypeWorkflows> TblTechUserTypeWorkflows { get; set; }
        public ICollection<TblUser> TblUser { get; set; }
    }
}
