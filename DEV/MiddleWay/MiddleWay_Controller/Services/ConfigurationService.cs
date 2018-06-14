using MiddleWay_Controller.ServiceInterfaces;
using MiddleWay_Controller.RepositoryInterfaces;
using MiddleWay_DTO.MiddleWay_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.Services
{
    public class ConfigurationService : IConfigurationService
    {
        #region Private Variables

        private IConfigurationRepository _configurationRepository;
        private IClientConfiguration _clientConfiguration;
        //private Dictionary<string, string> configuration { get; set; }

        #endregion Private Variables

        #region Constructor

        public ConfigurationService(IConfigurationRepository configurationRepository, IClientConfiguration clientConfiguration)
        {
            _configurationRepository = configurationRepository;
            _clientConfiguration = clientConfiguration;
        }

        #endregion Constructor

        #region Properties
        public bool HasConfiguration { get { return this._configurationRepository.HasConfigurations(_clientConfiguration.Client, _clientConfiguration.ProcessName); } }

        public string NotificationType { get { return GetConfigurationValueByName("NotificationType"); } }
        public string ElasticEmailApiKey { get { return GetConfigurationValueByName("ElasticEmailApiKey"); } }
        public string ElasticEmailAPIUrl { get { return GetConfigurationValueByName("ElasticEmailAPIUrl"); } }
        public string ElasticSMSAPIUrl { get { return GetConfigurationValueByName("ElasticSMSAPIUrl"); } }
        public string SqlServerDbMailProfileName { get { return GetConfigurationValueByName("SqlServerDbMailProfileName"); } }
        public string FromName { get { return GetConfigurationValueByName("FromName"); } }
        public string NotificationFrom { get { return GetConfigurationValueByName("NotificationFrom"); } }
        public string NotificationSentTo { get { return GetConfigurationValueByName("NotificationSentTo"); } }
        public string TIPWebConnection { get { return GetConfigurationValueByName("TIPWebConnection"); } }
        public string DataSourceType { get { return GetConfigurationValueByName("DataSourceType"); } }
        public string DataSourcePath { get { return GetConfigurationValueByName("DataSourcePath"); } }
        public string TextQualifier { get { return GetConfigurationValueByName("TextQualifier"); } }
        public string Delimiter { get { return GetConfigurationValueByName("Delimiter"); } }
        public string ExternalDataSourceConnection { get { return GetConfigurationValueByName("ExternalDataSourceConnection"); } }
        public string ReadBatchSize { get { return GetConfigurationValueByName("ReadBatchSize"); } }
        public string ExternalDataSourceQuerySelect { get { return GetConfigurationValueByName("ExternalDataSourceQuerySelect"); } }
        public string ExternalDataSourceQueryBody { get { return GetConfigurationValueByName("ExternalDataSourceQueryBody"); } }
        public string ExternalDataSourceQueryWhere { get { return GetConfigurationValueByName("ExternalDataSourceQueryWhere"); } }

        //public string  { get { return GetConfigurationByName(""); } }

        #endregion Properties

        #region Get Methods

        //public void ReadConfiguration()
        //{
        //    var configurations = _configurationRepository.GetConfiguration();

        //    foreach (var config in configurations)
        //    {
        //        configuration.Add(config.ConfigurationName, config.ConfigurationValue);
        //    }
        //}

        public ConfigurationsModel GetConfigurationByName(string name)
        {
            try
            {
                var configuration = this._configurationRepository.SelectConfigurationByName(_clientConfiguration.Client, _clientConfiguration.ProcessName, name);
                return configuration;
            }
            catch
            {
                throw;
            }
        }

        public string GetConfigurationValueByName(string name)
        {
            var configurationValue = this._configurationRepository.SelectConfigurationValueByName(_clientConfiguration.Client, _clientConfiguration.ProcessName, name);
            return configurationValue;
        }

        #endregion Get Methods
    }
}
