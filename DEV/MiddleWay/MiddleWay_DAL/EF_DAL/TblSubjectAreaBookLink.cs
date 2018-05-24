using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblSubjectAreaBookLink
    {
        public int SubjectAreaBookUid { get; set; }
        public int SubjectAreaUid { get; set; }
        public int BookInventoryUid { get; set; }
    }
}
