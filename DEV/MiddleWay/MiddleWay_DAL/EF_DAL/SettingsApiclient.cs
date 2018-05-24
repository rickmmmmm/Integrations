using System;
using System.Collections.Generic;

namespace MiddleWay_DAL.EF_DAL
{
    public partial class SettingsApiclient
    {
        public int SettingsApiclientUid { get; set; }
        public string Name { get; set; }
        public string SecretKey { get; set; }
        public string Passphrase { get; set; }
        public string BaseUrl { get; set; }
        public string Token { get; set; }
        public DateTime? TokenCreationDate { get; set; }
        public int? TokenExpiration { get; set; }
        public bool Enabled { get; set; }
    }
}
