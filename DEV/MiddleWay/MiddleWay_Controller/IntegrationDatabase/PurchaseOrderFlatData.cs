﻿using System;
using System.Collections.Generic;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public partial class PurchaseOrderFlatData
    {
        public int PurchaseOrderFlatDataUid { get; set; }
        public int ProcessUid { get; set; }
        public int RowId { get; set; }
        public string OrderNumber { get; set; }
        public string PurchaseDate { get; set; }
        public string LineNumber { get; set; }
        public string Status { get; set; }
        public string VendorName { get; set; }
        public string VendorAccountNumber { get; set; }
        public string SiteId { get; set; }
        public string SiteName { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductTypeDescription { get; set; }
        public string SiteAddedSiteId { get; set; }
        public string SiteAddedSiteName { get; set; }
        public string FundingSource { get; set; }
        public string FundingSourceDescription { get; set; }
        public string QuantityOrdered { get; set; }
        public string QuantityReceived { get; set; }
        public string PurchasePrice { get; set; }
        public string AccountCode { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentId { get; set; }
        public string Cfda { get; set; }
        public string IsAssociated { get; set; }
        public bool Rejected { get; set; }
        public string RejectedNotes { get; set; }

        public Processes ProcessU { get; set; }
    }
}
