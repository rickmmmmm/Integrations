using MiddleWay_Controller.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private Dictionary<string, string> configuration { get; set; }

        public ConfigurationService()
        {

        }

        public void ReadConfiguration()
        {

        }

        public string ApiKey { get { return GetConfigurationByName("ApiKey"); } }
        public string Delimiter { get { return GetConfigurationByName("Delimiter"); } }
        public string ElasticAPI { get { return GetConfigurationByName("ElasticAPI"); } }
        public string FromName { get { return GetConfigurationByName("FromName"); } }
        public string NotificationFrom { get { return GetConfigurationByName("NotificationFrom"); } }
        public string NotificationSentTo { get { return GetConfigurationByName("NotificationSentTo"); } }
        public string SMSAPI { get { return GetConfigurationByName("SMSAPI"); } }
        public string SqlServerDbMailProfileName { get { return GetConfigurationByName("SqlServerDbMailProfileName"); } }
        public string TextQualifier { get { return GetConfigurationByName("TextQualifier"); } }

        public string TIPWebConnection { get { return GetConfigurationByName("TIPWebConnection"); } }

        public string GetConfigurationByName(string name)
        {
            if (configuration.ContainsKey(name))
            {
                return configuration[name];
            }
            else
            {
                return null;
            }
        }

    }
}
