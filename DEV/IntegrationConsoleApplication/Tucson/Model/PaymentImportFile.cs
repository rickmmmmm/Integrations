using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PaymentImportFile
    {
        public int FineId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
    }
}
