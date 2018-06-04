using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.View_Models
{
   public class ChargePaymentsViewModel
    {
        //public ChargesModel ParentCharge { get; set; }
        public int ChargePaymentUID { get; set; }
        //public int ApplicationUID { get; set; }
        public int ChargeUID { get; set; }
        public int PaymentSiteUID { get; set; }
        public decimal ChargeAmount { get; set; }
        public bool Void { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public string PaidByUser { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
