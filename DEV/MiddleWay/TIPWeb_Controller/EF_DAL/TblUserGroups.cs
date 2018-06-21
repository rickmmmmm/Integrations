using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUserGroups
    {
        public int GroupId { get; set; }
        public string AccessCode { get; set; }
        public string Description { get; set; }
    }
}
