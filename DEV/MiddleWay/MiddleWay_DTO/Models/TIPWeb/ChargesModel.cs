using System;
using System.Collections.Generic;

namespace MiddleWay_DTO.Models.TIPWeb
{
    public class ChargeModel
    {
        public int ChargeUID { get; set; }
        public int ChargeTypeUID { get; set; }
        public int EntityUID { get; set; }
        public int EntityTypeUID { get; set; }
        public int ChargeSiteUID { get; set; }
        public decimal ChargeAmount { get; set; }
        public bool Void { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public string UniversalID { get; set; }
        public int ItemUID { get; set; }
        public int ItemTypeUID { get; set; }
        public DateTime DateSatisfied { get; set; }
        public int ApplicationUID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserID { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
