using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblRequisitionMultiCampus
    {
        public TblRequisitionMultiCampus()
        {
            TblRequisitionMultiCampusDetails = new HashSet<TblRequisitionMultiCampusDetails>();
            TblRequisitionMultiCampusLink = new HashSet<TblRequisitionMultiCampusLink>();
        }

        public int RequisitionMultiCampusUid { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public int StatusUid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedByUserId { get; set; }

        public TblStatus StatusU { get; set; }
        public ICollection<TblRequisitionMultiCampusDetails> TblRequisitionMultiCampusDetails { get; set; }
        public ICollection<TblRequisitionMultiCampusLink> TblRequisitionMultiCampusLink { get; set; }
    }
}
