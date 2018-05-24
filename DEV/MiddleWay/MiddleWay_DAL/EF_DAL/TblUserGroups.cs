using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUserGroups
    {
        public int GroupId { get; set; }
        public string AccessCode { get; set; }
        public string Description { get; set; }
    }
}
