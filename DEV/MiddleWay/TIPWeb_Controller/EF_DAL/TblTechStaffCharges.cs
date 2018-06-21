using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechStaffCharges
    {
        public int StaffChargeUid { get; set; }
        public string StaffId { get; set; }
        public int ChargeUid { get; set; }

        public TblUnvCharges ChargeU { get; set; }
    }
}
