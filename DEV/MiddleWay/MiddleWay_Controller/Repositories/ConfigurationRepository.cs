using MiddleWay_Controller.IntegrationDatabase;
using MiddleWay_DTO.RepositoryInterfaces.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MiddleWay_DTO.Models.MiddleWay;

namespace MiddleWay_Controller.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        #region Private Variables and Properties

        private IntegrationMiddleWayContext _context;
        //private IClientConfiguration _clientConfiguration;

        #endregion Private Variables and Properties

        #region Constructor

        public ConfigurationRepository(IntegrationMiddleWayContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Select Functions

        public bool HasConfigurations(string client, string processName)
        {
            try
            {
                var clientVal = (client ?? string.Empty).Trim().ToLower();
                var processNameVal = (processName ?? string.Empty).Trim().ToLower();

                var configurations = (from configuration in _context.Configurations
                                      join processes in _context.Processes
                                        on configuration.ProcessUid equals processes.ProcessUid
                                      where configuration.Enabled
                                         && processes.Client.Trim().ToLower() == clientVal
                                         && processes.ProcessName.Trim().ToLower() == processNameVal
                                      select 1).Count();

                return configurations > 0;
            }
            catch
            {
                throw;
            }
        }

        public ConfigurationsModel SelectConfigurationByName(string client, string processName, string name)
        {
            try
            {
                var clientVal = (client ?? string.Empty).Trim().ToLower();
                var processNameVal = (processName ?? string.Empty).Trim().ToLower();
                var nameVal = (name ?? string.Empty).Trim().ToLower();

                var config = (from configuration in _context.Configurations
                              join processes in _context.Processes
                                on configuration.ProcessUid equals processes.ProcessUid
                              where configuration.ConfigurationName.Trim().ToLower() == nameVal
                                 && processes.Client.Trim().ToLower() == clientVal
                                 && processes.ProcessName.Trim().ToLower() == processNameVal
                              select new ConfigurationsModel
                              {
                                  ConfigurationUid = configuration.ConfigurationUid,
                                  ProcessUid = configuration.ProcessUid,
                                  ConfigurationName = configuration.ConfigurationName,
                                  ConfigurationValue = configuration.ConfigurationValue,
                                  Enabled = configuration.Enabled
                              }).FirstOrDefault();

                return config;
            }
            catch
            {
                throw;
            }
        }

        public string SelectConfigurationValueByName(string client, string processName, string name)
        {
            try
            {
                var clientVal = (client ?? string.Empty).Trim().ToLower();
                var processNameVal = (processName ?? string.Empty).Trim().ToLower();
                var nameVal = (name ?? string.Empty).Trim().ToLower();

                var configurationValue = (from configuration in _context.Configurations
                                          join processes in _context.Processes
                                            on configuration.ProcessUid equals processes.ProcessUid
                                          where configuration.ConfigurationName.Trim().ToLower() == nameVal
                                             && processes.Client.Trim().ToLower() == clientVal
                                             && processes.ProcessName.Trim().ToLower() == processNameVal
                                          select configuration.ConfigurationValue
                                      ).FirstOrDefault();

                return configurationValue;
            }
            catch
            {
                throw;
            }
        }

        #endregion Select Functions

        #region Insert Functions

        #endregion Insert Functions

        #region Update Functions

        #endregion Update Functions

        #region Delete Functions

        #endregion Delete Functions
    }
}
