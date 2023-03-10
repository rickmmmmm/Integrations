using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblPaymentsCollected
    {
        public int PaymentId { get; set; }
        public int Id { get; set; }
        public string Isbn { get; set; }
        public string Accession { get; set; }
        public string Code { get; set; }
        public decimal? Price { get; set; }
        public int? Copies { get; set; }
        public decimal? TotalOwed { get; set; }
        public decimal? TotalPaid { get; set; }
        public int UserId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Notes { get; set; }
        public bool Archived { get; set; }
    }
}
