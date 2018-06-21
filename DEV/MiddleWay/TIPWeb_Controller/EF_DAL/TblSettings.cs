using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblSettings
    {
        public string DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string ShippingAddress1 { get; set; }
        public string ShippingAddress2 { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingZip { get; set; }
        public string ShippingPhone { get; set; }
        public string ShippingFax { get; set; }
        public string ShippingContact { get; set; }
        public string ShippingTitle { get; set; }
        public string ShippingNotes { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public string BillingPhone { get; set; }
        public string BillingFax { get; set; }
        public string BillingContact { get; set; }
        public string BillingTitle { get; set; }
        public string BillingNotes { get; set; }
    }
}
