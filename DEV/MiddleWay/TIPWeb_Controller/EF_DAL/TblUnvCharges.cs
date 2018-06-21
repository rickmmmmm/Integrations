using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUnvCharges
    {
        public TblUnvCharges()
        {
            TblTechAccessoryCharges = new HashSet<TblTechAccessoryCharges>();
            TblTechStaffCharges = new HashSet<TblTechStaffCharges>();
            TblTechStudentCharges = new HashSet<TblTechStudentCharges>();
            TblUnvChargePayments = new HashSet<TblUnvChargePayments>();
        }

        public int ChargeUid { get; set; }
        public int ChargeTypeUid { get; set; }
        public int EntityUid { get; set; }
        public int EntityTypeUid { get; set; }
        public int? ChargeSiteUid { get; set; }
        public decimal ChargeAmount { get; set; }
        public bool Void { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public string UniversalId { get; set; }
        public int ItemUid { get; set; }
        public int ItemTypeUid { get; set; }
        public DateTime? DateSatisfied { get; set; }
        public int ApplicationUid { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public TblUnvChargeTypes ChargeTypeU { get; set; }
        public ICollection<TblTechAccessoryCharges> TblTechAccessoryCharges { get; set; }
        public ICollection<TblTechStaffCharges> TblTechStaffCharges { get; set; }
        public ICollection<TblTechStudentCharges> TblTechStudentCharges { get; set; }
        public ICollection<TblUnvChargePayments> TblUnvChargePayments { get; set; }
    }
}
