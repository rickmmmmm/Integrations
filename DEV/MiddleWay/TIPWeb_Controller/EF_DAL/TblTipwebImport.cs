using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblTipwebImport
    {
        public int TipwebImportId { get; set; }
        public string StepName { get; set; }
        public string ImportStatus { get; set; }
        public int? RowsImported { get; set; }
        public string ImportType { get; set; }
        public string Notes { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
