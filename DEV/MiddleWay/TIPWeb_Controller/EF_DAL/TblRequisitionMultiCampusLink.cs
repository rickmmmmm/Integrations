using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblRequisitionMultiCampusLink
    {
        public int RequisitionToMultiCampusRequisitionUid { get; set; }
        public int RequisitionUid { get; set; }
        public int RequisitionMultiCampusUid { get; set; }

        public TblRequisitionMultiCampus RequisitionMultiCampusU { get; set; }
        public TblRequisitions RequisitionU { get; set; }
    }
}
