using MiddleWay_Controller.IntegrationDatabase;
using MiddleWay_DTO.MiddleWay_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.RepositoryInterfaces
{
    public interface IConfigurationRepository
    {
        bool HasConfigurations(string client, string processName);

        ConfigurationsModel SelectConfigurationByName(string client, string processName, string name);

        string SelectConfigurationValueByName(string client, string processName, string name);

    }
}
