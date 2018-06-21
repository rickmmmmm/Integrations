using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class EtlPurchases
    {
        public int EtlpurchaseUid { get; set; }
        public string PurchaseOrder { get; set; }
        public string Vendor { get; set; }
        public string VendorAccountNumber { get; set; }
        public string Site { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Notes { get; set; }
        public string ProductType { get; set; }
        public string Product { get; set; }
        public string ProductDescription { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string Area { get; set; }
        public bool ProductByNumber { get; set; }
        public string FundingSource { get; set; }
        public string FundingSourceDescription { get; set; }
        public string SiteAdded { get; set; }
        public int? QuantityOrdered { get; set; }
        public int? QuantityReceived { get; set; }
        public decimal? PurchasePrice { get; set; }
        public string AccountCode { get; set; }
        public string Department { get; set; }
        public int LineNumber { get; set; }
        public string ShippedToSite { get; set; }
        public int? QuantityShipped { get; set; }
        public int? TicketNumber { get; set; }
        public DateTime? TicketedDate { get; set; }
        public string TicketedBy { get; set; }
        public int? PurchaseUid { get; set; }
        public int? VendorUid { get; set; }
        public int? SiteUid { get; set; }
        public int? PurchaseItemDetailUid { get; set; }
        public int? ItemUid { get; set; }
        public int? FundingSourceUid { get; set; }
        public int? SiteAddedSiteUid { get; set; }
        public int? TechDepartmentUid { get; set; }
        public int? PurchaseItemShipmentUid { get; set; }
        public int? ShippedToSiteUid { get; set; }
        public int? TicketedByUserId { get; set; }
        public string Other1 { get; set; }
        public bool New { get; set; }
        public bool? PoClose { get; set; }
        public bool? PolineClose { get; set; }
    }
}
