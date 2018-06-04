using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.View_Models
{
    public class ChargesViewModel
    {
        public int ChargeUID { get; set; }
        public decimal ChargeAmount { get; set; }
        public ICollection<ChargePaymentsViewModel> Payments { get; set; }
    }
}
