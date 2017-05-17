using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ChargePayments
    {
        public Charge ParentCharge { get; set; }
        public decimal ChargeAmount { get; set; }
        public bool Void { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
