using MiddleWay_DTO.Models.MiddleWay;
using System.Collections.Generic;

namespace MiddleWay_DTO.RepositoryInterfaces.MiddleWay
{
    public interface IMappingsRepository
    {
        List<MappingsModel> SelectMappings(int processUid, string stepName);
        List<MappingsModel> SelectMappings(string client, string processName, string stepName);
        MappingsModel SelectMappings(int processUid, string stepName, string sourceColumn);
        MappingsModel SelectMappings(string client, string processName, string stepName, string sourceColumn);
    }
}
