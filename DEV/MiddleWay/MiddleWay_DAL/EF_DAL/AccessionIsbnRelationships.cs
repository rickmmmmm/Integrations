using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class AccessionIsbnRelationships
    {
        public int AccessionIsbnRelationshipUid { get; set; }
        public int BookInventoryUid { get; set; }
        public string Accession { get; set; }

        public TblBookInventory BookInventoryU { get; set; }
    }
}
