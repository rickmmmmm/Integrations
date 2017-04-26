using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Charge
    {
        public int ChargeUID { get; set; }
        public decimal? ChargeAmount { get; set; }
        public ICollection<ChargePayments> Payments { get; set; }
    }
}
