using System;
using System.Collections.Generic;

namespace TIPWeb_Controller.EF_DAL
{
    public partial class TblUserPreferences
    {
        public int UserId { get; set; }
        public int? GeneralListLimit { get; set; }
        public int? BookListLimit { get; set; }
        public bool? Login { get; set; }
        public bool? DistributionWarning { get; set; }
        public string Enrollment { get; set; }
        public string Other1 { get; set; }
        public string Other2 { get; set; }
        public bool? ExpandedView { get; set; }
        public bool? GcodeDefault { get; set; }
        public bool? BackupPrompt { get; set; }
        public bool? UseAccessions { get; set; }
        public bool? AccessionWarning1 { get; set; }
        public bool? AccessionWarning2 { get; set; }
        public bool? QuotaWarning { get; set; }
        public int? QuotaAdjustment { get; set; }
        public string NetworkPath { get; set; }
        public string Host { get; set; }
        public bool? Sounds { get; set; }
        public bool? Verification { get; set; }
        public bool? SetReqNumber { get; set; }
        public int? StartingNumber { get; set; }
        public string SpecifyReqNumber { get; set; }
        public string LastReqNumber { get; set; }
        public string PickTicket { get; set; }
        public string FindAbook { get; set; }
        public bool? PromptRecord { get; set; }
        public string LeftMargin { get; set; }
        public string TopMargin { get; set; }
        public string BarCodeText { get; set; }
        public DateTime? RepairDay { get; set; }
        public DateTime? RepairTime { get; set; }
        public DateTime? Maintenance { get; set; }
        public bool? DisableRepair { get; set; }
        public string SearchRelate1 { get; set; }
        public string SearchRelate2 { get; set; }
        public bool? SetAdjNumber { get; set; }
        public string SpecifyAdjNumber { get; set; }
        public string LastAdjNumber { get; set; }
        public int? StartingNumber2 { get; set; }
        public string AdjustmentPrefix { get; set; }
        public bool? ExplainNotice { get; set; }
        public bool? Notice { get; set; }
        public bool? DuplicateCheck { get; set; }
        public string DistributeSearch1 { get; set; }
        public string DistributeSearch2 { get; set; }
    }
}
