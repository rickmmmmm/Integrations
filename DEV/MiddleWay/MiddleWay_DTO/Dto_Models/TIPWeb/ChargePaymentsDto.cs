using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models
{
    public class ChargePaymentsDto
    {
        //public ChargesModel ParentCharge { get; set; }
        public int ChargeUID { get; set; }
        public string PaymentSiteID { get; set; }
        public decimal ChargeAmount { get; set; }
        public bool Void { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
