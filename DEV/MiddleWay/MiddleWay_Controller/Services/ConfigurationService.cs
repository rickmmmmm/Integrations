using MiddleWay_Controller.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.Services
{
    public class ConfigurationService : IConfigurationService
    {
        #region Private Variables

        private IConfigurationRepository _configurationRepository;
        private Dictionary<string, string> configuration { get; set; }

        #endregion Private Variables and Properties

        #region Constructor

        public ConfigurationService(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        #endregion Constructor

        #region Properties

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

        #endregion Properties

        #region Get Methods

        public void ReadConfiguration()
        {
            var configurations = _configurationRepository.GetConfiguration();

            foreach (var config in configurations)
            {
                configuration.Add(config.ConfigurationName, config.ConfigurationValue);
            }
        }

        public string GetConfigurationByName(string name)
        {
            if (configuration.Count > 0)
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
            else
            {
                throw new NotImplementedException("Configuration not loaded");
            }
        }

        #endregion Get Methods
    }
}
