using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechTemplateFunction
    {
        public int TemplateFunctionUid { get; set; }
        public int PermissionTemplateUid { get; set; }
        public int FunctionUid { get; set; }
        public bool Active { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblTechPermissionTemplate PermissionTemplateU { get; set; }
    }
}
