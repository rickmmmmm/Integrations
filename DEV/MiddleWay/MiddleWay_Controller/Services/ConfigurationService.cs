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

        #endregion Private Variables

        #region Constructor

        public ConfigurationService(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        #endregion Constructor

        #region Properties
        public bool IsConfigurationLoaded { get { return configuration != null && configuration.Count > 0; } }

        public string NotificationType { get { return GetConfigurationByName("NotificationType"); } }
        public string ElasticEmailApiKey { get { return GetConfigurationByName("ElasticEmailApiKey"); } }
        public string ElasticEmailAPIUrl { get { return GetConfigurationByName("ElasticEmailAPIUrl"); } }
        public string ElasticSMSAPIUrl { get { return GetConfigurationByName("ElasticSMSAPIUrl"); } }
        public string SqlServerDbMailProfileName { get { return GetConfigurationByName("SqlServerDbMailProfileName"); } }
        public string FromName { get { return GetConfigurationByName("FromName"); } }
        public string NotificationFrom { get { return GetConfigurationByName("NotificationFrom"); } }
        public string NotificationSentTo { get { return GetConfigurationByName("NotificationSentTo"); } }
        public string TIPWebConnection { get { return GetConfigurationByName("TIPWebConnection"); } }
        public string DataSourceType { get { return GetConfigurationByName("DataSourceType"); } }
        public string DataSourcePath { get { return GetConfigurationByName("DataSourcePath"); } }
        public string TextQualifier { get { return GetConfigurationByName("TextQualifier"); } }
        public string Delimiter { get { return GetConfigurationByName("Delimiter"); } }
        public string ExternalDataSourceConnection { get { return GetConfigurationByName("ExternalDataSourceConnection"); } }
        public string ReadBatchSize { get { return GetConfigurationByName("ReadBatchSize"); } }
        public string ExternalDataSourceQuerySelect { get { return GetConfigurationByName("ExternalDataSourceQuerySelect"); } }
        public string ExternalDataSourceQueryBody { get { return GetConfigurationByName("ExternalDataSourceQueryBody"); } }
        public string ExternalDataSourceQueryWhere { get { return GetConfigurationByName("ExternalDataSourceQueryWhere"); } }
        //public string  { get { return GetConfigurationByName(""); } }

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
