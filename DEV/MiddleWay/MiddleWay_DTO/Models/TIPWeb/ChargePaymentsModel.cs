using System;

namespace MiddleWay_DTO.Models.TIPWeb
{
    public class ChargePaymentsModel
    {
        //public ChargesModel ParentCharge { get; set; }
        public int ChargePaymentUid { get; set; }
        public int ApplicationUid { get; set; }
        public int ChargeUid { get; set; }
        public int PaymentSiteUid { get; set; }
        public decimal ChargeAmount { get; set; }
        public bool Void { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
