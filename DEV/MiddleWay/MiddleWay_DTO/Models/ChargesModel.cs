using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWay_DTO.Models
{
    public class Charge
    {
        public int ChargeUID { get; set; }
        public decimal? ChargeAmount { get; set; }
        public ICollection<ChargePayments> Payments { get; set; }
    }

    public class ChargeExportFile
    {
        public int FineId { get; set; }
        public string StudentId { get; set; }
        public string ItemTitle { get; set; }
        public string ItemBarcode { get; set; }
        public string ItemCollection { get; set; }
        public string FineLocationCode { get; set; }
        public string FineDescription { get; set; }
        public DateTime FineCreatedDate { get; set; }
        public decimal FineAmount { get; set; }

    }

    public class ChargePayments
    {
        public Charge ParentCharge { get; set; }
        public decimal ChargeAmount { get; set; }
        public bool Void { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
