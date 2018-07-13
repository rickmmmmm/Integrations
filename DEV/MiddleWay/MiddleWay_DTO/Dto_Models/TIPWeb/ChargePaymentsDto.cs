using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.DTO_Models.TIPWeb
{
    public class ChargePaymentsDto
    {
        //public ChargesModel ParentCharge { get; set; }
        public int ChargeUid { get; set; }
        public string PaymentSiteId { get; set; }
        public decimal ChargeAmount { get; set; }
        public bool Void { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
