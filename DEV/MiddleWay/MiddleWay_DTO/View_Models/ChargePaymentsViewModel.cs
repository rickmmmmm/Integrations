using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.View_Models
{
   public class ChargePaymentsViewModel
    {
        //public ChargesModel ParentCharge { get; set; }
        public int ChargePaymentUid { get; set; }
        //public int ApplicationUid { get; set; }
        public int ChargeUid { get; set; }
        public int PaymentSiteUid { get; set; }
        public decimal ChargeAmount { get; set; }
        public bool Void { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public string PaidByUser { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
