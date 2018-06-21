using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechStudentCharges
    {
        public int StudentChargeUid { get; set; }
        public string StudentId { get; set; }
        public int ChargeUid { get; set; }

        public TblUnvCharges ChargeU { get; set; }
    }
}
