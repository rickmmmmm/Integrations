using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblRequisitionMultiCampusDetails
    {
        public int RequisitionMultiCampusDetailUid { get; set; }
        public int RequisitionMultiCampusUid { get; set; }
        public int CampusUid { get; set; }
        public int BookInventoryUid { get; set; }
        public int Needed { get; set; }
        public int Ordered { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedByUserId { get; set; }

        public TblRequisitionMultiCampus RequisitionMultiCampusU { get; set; }
    }
}
