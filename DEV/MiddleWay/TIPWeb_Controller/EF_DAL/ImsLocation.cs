using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class ImsLocation
    {
        public int ImsLocationId { get; set; }
        public string LocationId { get; set; }
        public string AltLocationId { get; set; }
        public string LocationName { get; set; }
        public bool? Active { get; set; }
    }
}
