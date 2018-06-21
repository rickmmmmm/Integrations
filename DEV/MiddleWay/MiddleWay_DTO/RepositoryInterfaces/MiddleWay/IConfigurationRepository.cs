//using MiddleWay_Controller.IntegrationDatabase;
using MiddleWay_DTO.Models.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.RepositoryInterfaces.MiddleWay
{
    public interface IConfigurationRepository
    {
        bool HasConfigurations(string client, string processName);

        ConfigurationsModel SelectConfigurationByName(string client, string processName, string name);

        string SelectConfigurationValueByName(string client, string processName, string name);

    }
}
