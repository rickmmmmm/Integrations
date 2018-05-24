using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class TblUnvApplications
    {
        public TblUnvApplications()
        {
            TblFundingSources = new HashSet<TblFundingSources>();
            TblTechFundingSourceUsers = new HashSet<TblTechFundingSourceUsers>();
            TblUnvAlertTypes = new HashSet<TblUnvAlertTypes>();
            TblUnvAlerts = new HashSet<TblUnvAlerts>();
            TblUnvAudits = new HashSet<TblUnvAudits>();
            TblUnvClosings = new HashSet<TblUnvClosings>();
            TblUnvDistrictPreferences = new HashSet<TblUnvDistrictPreferences>();
            TblUnvRecipient = new HashSet<TblUnvRecipient>();
            TblUnvRecipientInformation = new HashSet<TblUnvRecipientInformation>();
            TblUnvRecipientType = new HashSet<TblUnvRecipientType>();
            TblUnvScheduleReportType = new HashSet<TblUnvScheduleReportType>();
            TblUnvSupportLinks = new HashSet<TblUnvSupportLinks>();
            TblUser = new HashSet<TblUser>();
            TblVendor = new HashSet<TblVendor>();
        }

        public int ApplicationUid { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationDescription { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int LastModifiedByUserId { get; set; }

        public ICollection<TblFundingSources> TblFundingSources { get; set; }
        public ICollection<TblTechFundingSourceUsers> TblTechFundingSourceUsers { get; set; }
        public ICollection<TblUnvAlertTypes> TblUnvAlertTypes { get; set; }
        public ICollection<TblUnvAlerts> TblUnvAlerts { get; set; }
        public ICollection<TblUnvAudits> TblUnvAudits { get; set; }
        public ICollection<TblUnvClosings> TblUnvClosings { get; set; }
        public ICollection<TblUnvDistrictPreferences> TblUnvDistrictPreferences { get; set; }
        public ICollection<TblUnvRecipient> TblUnvRecipient { get; set; }
        public ICollection<TblUnvRecipientInformation> TblUnvRecipientInformation { get; set; }
        public ICollection<TblUnvRecipientType> TblUnvRecipientType { get; set; }
        public ICollection<TblUnvScheduleReportType> TblUnvScheduleReportType { get; set; }
        public ICollection<TblUnvSupportLinks> TblUnvSupportLinks { get; set; }
        public ICollection<TblUser> TblUser { get; set; }
        public ICollection<TblVendor> TblVendor { get; set; }
    }
}
