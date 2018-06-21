using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUnvSerializedObjects
    {
        public int Id { get; set; }
        public int ApplicationUid { get; set; }
        public int UserUid { get; set; }
        public string SerilalizedObj { get; set; }
        public int ObjectTypeUid { get; set; }
        public int? SiteUid { get; set; }
        public int CreatedByUserUid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserUid { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
