using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblMasterPublishers
    {
        public TblMasterPublishers()
        {
            TblMasterBooks = new HashSet<TblMasterBooks>();
        }

        public int PublisherId { get; set; }
        public string PublisherName { get; set; }

        public ICollection<TblMasterBooks> TblMasterBooks { get; set; }
    }
}
