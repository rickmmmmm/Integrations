using MiddleWay_DTO.Enumerations;
using MiddleWay_DTO.Models.MiddleWay;
using System.Collections.Generic;

namespace MiddleWay_DTO.RepositoryInterfaces.MiddleWay
{
    public interface IMappingsRepository
    {
        List<MappingsModel> SelectMappings(int processUid, ProcessSteps stepName);
        List<MappingsModel> SelectMappings(string client, string processName, ProcessSteps stepName);
        MappingsModel SelectMappings(int processUid, ProcessSteps stepName, string sourceColumn);
        MappingsModel SelectMappings(string client, string processName, ProcessSteps stepName, string sourceColumn);
    }
}
