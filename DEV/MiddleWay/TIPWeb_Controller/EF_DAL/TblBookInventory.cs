using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblBookInventory
    {
        public TblBookInventory()
        {
            AccessionIsbnRelationships = new HashSet<AccessionIsbnRelationships>();
            BookImages = new HashSet<BookImages>();
            TblAuditBookInventory = new HashSet<TblAuditBookInventory>();
            TblAuditDetailCounts = new HashSet<TblAuditDetailCounts>();
            TblBooksCourses = new HashSet<TblBooksCourses>();
            TblBooksCoursesDistrict = new HashSet<TblBooksCoursesDistrict>();
            TblDigitalMaterialDetails = new HashSet<TblDigitalMaterialDetails>();
            TblVendorOrderDetails = new HashSet<TblVendorOrderDetails>();
        }

        public string Isbn { get; set; }
        public string Slc { get; set; }
        public string GroupCode { get; set; }
        public string Title { get; set; }
        public decimal? Price { get; set; }
        public int? Publisher { get; set; }
        public string Grade { get; set; }
        public string Adopt { get; set; }
        public string DistReqCode { get; set; }
        public string Expire { get; set; }
        public string Copyright { get; set; }
        public int? SetIsbn { get; set; }
        public bool? BookSet { get; set; }
        public int? LeftInStorage { get; set; }
        public int? DistrictOnOrder { get; set; }
        public int? StateOnOrder { get; set; }
        public int? OnOrder { get; set; }
        public bool? ShowOnReports { get; set; }
        public string BinId { get; set; }
        public int UserId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool? CampusAdded { get; set; }
        public string Notes { get; set; }
        public string Udf { get; set; }
        public bool? Active { get; set; }
        public int BookInventoryUid { get; set; }
        public DateTime? CreatedDate { get; set; }

        public ICollection<AccessionIsbnRelationships> AccessionIsbnRelationships { get; set; }
        public ICollection<BookImages> BookImages { get; set; }
        public ICollection<TblAuditBookInventory> TblAuditBookInventory { get; set; }
        public ICollection<TblAuditDetailCounts> TblAuditDetailCounts { get; set; }
        public ICollection<TblBooksCourses> TblBooksCourses { get; set; }
        public ICollection<TblBooksCoursesDistrict> TblBooksCoursesDistrict { get; set; }
        public ICollection<TblDigitalMaterialDetails> TblDigitalMaterialDetails { get; set; }
        public ICollection<TblVendorOrderDetails> TblVendorOrderDetails { get; set; }
    }
}
