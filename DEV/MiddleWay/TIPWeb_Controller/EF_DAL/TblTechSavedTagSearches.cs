using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTechSavedTagSearches
    {
        public int TagSearchUid { get; set; }
        public string SearchName { get; set; }
        public int SiteUid { get; set; }
        public string BasicFilters { get; set; }
        public string AdvancedFilters { get; set; }
        public string GridSettings { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
    }
}
