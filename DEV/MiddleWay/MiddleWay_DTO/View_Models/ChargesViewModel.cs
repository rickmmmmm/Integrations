using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.View_Models
{
    public class ChargesViewModel
    {
        public int ChargeUid { get; set; }
        public decimal ChargeAmount { get; set; }
        public ICollection<ChargePaymentsViewModel> Payments { get; set; }
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
}
