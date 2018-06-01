using System;

namespace MiddleWay_DTO.TIPWeb_Models
{
    public class ChargePaymentsModel
    {
        public ChargesModel ParentCharge { get; set; }
        public decimal ChargeAmount { get; set; }
        public bool Void { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
