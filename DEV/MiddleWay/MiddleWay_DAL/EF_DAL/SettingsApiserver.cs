using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class SettingsApiserver
    {
        public int SettingsApiserverUid { get; set; }
        public string Name { get; set; }
        public string SecretKey { get; set; }
        public string Passphrase { get; set; }
        public DateTime? ValidFromDate { get; set; }
        public DateTime? ValidToDate { get; set; }
        public bool Enabled { get; set; }
        public int SettingsApirolesId { get; set; }
    }
}
