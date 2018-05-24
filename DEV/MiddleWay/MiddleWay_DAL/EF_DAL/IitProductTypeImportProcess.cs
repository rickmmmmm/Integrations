using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class IitProductTypeImportProcess
    {
        public int ProductTypeImportProcessUid { get; set; }
        public int RowNumber { get; set; }
        public string ProductType { get; set; }
        public int ProductTypeUid { get; set; }
        public string CustomFieldName1 { get; set; }
        public string CustomFieldDataType1 { get; set; }
        public bool? CustomFieldRequired1 { get; set; }
        public int? InventoryMetaUid1 { get; set; }
        public string CustomFieldName2 { get; set; }
        public string CustomFieldDataType2 { get; set; }
        public bool? CustomFieldRequired2 { get; set; }
        public int? InventoryMetaUid2 { get; set; }
        public string CustomFieldName3 { get; set; }
        public string CustomFieldDataType3 { get; set; }
        public bool? CustomFieldRequired3 { get; set; }
        public int? InventoryMetaUid3 { get; set; }
        public string CustomFieldName4 { get; set; }
        public string CustomFieldDataType4 { get; set; }
        public bool? CustomFieldRequired4 { get; set; }
        public int? InventoryMetaUid4 { get; set; }
    }
}
