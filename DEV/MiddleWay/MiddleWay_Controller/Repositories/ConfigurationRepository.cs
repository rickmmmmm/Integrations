using MiddleWay_Controller.IntegrationDatabase;
using MiddleWay_Controller.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MiddleWay_Controller.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        #region Private Variables and Properties

        private IntegrationMiddleWayContext _context;

        #endregion Private Variables and Properties

        #region Constructor

        public ConfigurationRepository(IntegrationMiddleWayContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Select Functions

        public List<Configurations> GetConfiguration()
        {
            try
            {
                var configurations = (from configuration in _context.Configurations
                                      where configuration.Enabled
                                      select configuration).ToList();

                return configurations;
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
