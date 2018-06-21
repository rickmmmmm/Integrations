using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUnvItemTypes
    {
        public int ItemTypeUid { get; set; }
        public string Description { get; set; }
        public string TableName { get; set; }
        public string IdentityColumn { get; set; }
        public int ApplicationUid { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
