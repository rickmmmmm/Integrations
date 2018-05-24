using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblDistrictPreferences
    {
        public long HighestAccession { get; set; }
        public int RowsDisplayed { get; set; }
        public int TransferOptions { get; set; }
        public bool AllowCampusInitXfer { get; set; }
        public int StudIdlimitMin { get; set; }
        public int StudIdlimitMax { get; set; }
        public int TeachIdlimitMin { get; set; }
        public int TeachIdlimitMax { get; set; }
        public int IsbnlimitMin { get; set; }
        public int IsbnlimitMax { get; set; }
        public int AccLimitMin { get; set; }
        public int AccLimitMax { get; set; }
        public bool? ShowId { get; set; }
        public bool ShowAlphaNum { get; set; }
        public bool? AllowSound { get; set; }
        public bool ValidateAccession { get; set; }
        public int ForecastPercent { get; set; }
        public bool AllowSingleBarCode { get; set; }
        public bool PrintTicketHideComponents { get; set; }
        public bool PrintTicketHideDenied { get; set; }
        public int DistrictPreferenceId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string PickTicketMessage { get; set; }
        public bool? TextbookBuyBack { get; set; }
        public byte[] DistrictLogo { get; set; }
        public int? PickTicketExpirationDays { get; set; }
    }
}
