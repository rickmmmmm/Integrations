using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.Models.MiddleWay
{
    public class PurchaseInvoiceFlatDataModel
    {
        public int PurchaseInvoiceFlatDataUID { get; set; }
        public int ProcessUid { get; set; }
        public int OrderNumber { get; set; }
        public int LineNumber { get; set; }
        public int InvoiceNumber { get; set; }
        public int InvoiceDate { get; set; }
        public int InvoiceStatus { get; set; }
        public int AuthorizationStatus { get; set; }
        public int AccountingDate { get; set; }
        public int LineDescription { get; set; }
        public int AssetPrice { get; set; }
        public int InvoicePrice { get; set; }
        public int Quantity { get; set; }
        public int LineAmount { get; set; }
        public int RowID { get; set; }
    }
}
