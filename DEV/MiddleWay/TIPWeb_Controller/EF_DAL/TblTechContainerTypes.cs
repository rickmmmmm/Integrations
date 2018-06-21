using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechContainerTypes
    {
        public TblTechContainerTypes()
        {
            TblTechContainers = new HashSet<TblTechContainers>();
        }

        public int ContainerTypeUid { get; set; }
        public string ContainerTypeName { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public ICollection<TblTechContainers> TblTechContainers { get; set; }
    }
}
