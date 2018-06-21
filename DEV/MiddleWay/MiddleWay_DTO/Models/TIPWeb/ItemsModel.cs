using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWay_DTO.Models.TIPWeb
{
    public class ItemsModel
    {
        public string ItemNumber { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int ItemType { get; set; }
        public string ModelNumber { get; set; }
        public int ManufacturerUID { get; set; }
        public decimal? ItemSuggestedPrice { get; set; }
        public int AreaUID { get; set; }
        public string ItemNotes { get; set; }
        public string SKU { get; set; }
        public bool SerialRequired { get; set; }
        public int ProjectedLife { get; set; }
        public string CustomField1 { get; set; }
        public string CustomField2 { get; set; }
        public string CustomField3 { get; set; }
        public bool Active { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserID { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool AllowUntagged { get; set; }
    }
}
