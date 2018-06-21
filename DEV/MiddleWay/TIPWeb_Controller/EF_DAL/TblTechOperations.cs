using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechOperations
    {
        public TblTechOperations()
        {
            TblTechInventoryHistory = new HashSet<TblTechInventoryHistory>();
        }

        public int OperationUid { get; set; }
        public string OperationName { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public ICollection<TblTechInventoryHistory> TblTechInventoryHistory { get; set; }
    }
}
