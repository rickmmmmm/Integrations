using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblVendor
    {
        public TblVendor()
        {
            TblTechPurchases = new HashSet<TblTechPurchases>();
            TblVendorBooks = new HashSet<TblVendorBooks>();
        }

        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string AccountNumber { get; set; }
        public string CampusId { get; set; }
        public string Notes { get; set; }
        public bool? Active { get; set; }
        public int UserId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ApplicationUid { get; set; }

        public TblUnvApplications ApplicationU { get; set; }
        public ICollection<TblTechPurchases> TblTechPurchases { get; set; }
        public ICollection<TblVendorBooks> TblVendorBooks { get; set; }
    }
}
