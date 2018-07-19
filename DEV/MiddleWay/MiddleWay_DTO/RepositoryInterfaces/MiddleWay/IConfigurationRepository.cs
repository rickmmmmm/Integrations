using MiddleWay_DTO.Models.MiddleWay;
using System.Collections.Generic;

namespace MiddleWay_DTO.RepositoryInterfaces.MiddleWay
{
    public interface IConfigurationRepository
    {
        bool HasConfigurations(string client, string processName);

        ConfigurationsModel SelectConfigurationByName(string client, string processName, string name);

        string SelectConfigurationValueByName(string client, string processName, string name);

        List<ConfigurationsModel> SelectConfigurations(string client, string processName);

    }
}
