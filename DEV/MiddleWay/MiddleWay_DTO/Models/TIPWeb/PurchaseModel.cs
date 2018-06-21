﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWay_DTO.Models.TIPWeb
{
    public class PurchaseModel
    {
        public int StatusUID { get; set; }
        public int VendorUID { get; set; }
        public int SiteID { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string Other1 { get; set; }
        public ICollection<PurchaseItemDetailModel> PurchaseOrderDetails { get; set; }
    }
}
