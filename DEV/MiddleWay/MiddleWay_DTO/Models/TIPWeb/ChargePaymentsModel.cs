using System;

namespace MiddleWay_DTO.Models.TIPWeb
{
    public class ChargePaymentsModel
    {
        //public ChargesModel ParentCharge { get; set; }
        public int ChargePaymentUID { get; set; }
        public int ApplicationUID { get; set; }
        public int ChargeUID { get; set; }
        public int PaymentSiteUID { get; set; }
        public decimal ChargeAmount { get; set; }
        public bool Void { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserID { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
