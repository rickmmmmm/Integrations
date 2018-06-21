using MiddleWay_DTO.ServiceInterfaces.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.Services
{
    public class ClientConfiguration : IClientConfiguration
    {
        #region Variables

        private string _client;
        private string _processName;

        #endregion Variables

        #region Properties

        public string Client
        {
            get
            {
                return _client;
            }
        }

        public string ProcessName
        {
            get
            {
                return _processName;
            }
        }

        #endregion Properties

        #region Constructor
        public ClientConfiguration(string client, string processName)
        {
            _client = client;
            _processName = processName;
        }

        #endregion Constructor

        #region Get Methods

        #endregion Get Methods
    }
}
