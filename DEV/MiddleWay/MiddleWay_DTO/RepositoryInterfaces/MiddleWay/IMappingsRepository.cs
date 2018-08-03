using MiddleWay_DTO.Enumerations;
using MiddleWay_DTO.Models.MiddleWay_Controller;
using System.Collections.Generic;

namespace MiddleWay_DTO.RepositoryInterfaces.MiddleWay
{
    public interface IMappingsRepository
    {
        bool HasMappings(int processUid, ProcessSteps stepName);
        bool HasMappings(string client, string processName, ProcessSteps stepName);
        List<MappingsModel> SelectMappings(int processUid, ProcessSteps stepName);
        List<MappingsModel> SelectMappings(string client, string processName, ProcessSteps stepName);
        MappingsModel SelectMappings(int processUid, ProcessSteps stepName, string sourceColumn);
        MappingsModel SelectMappings(string client, string processName, ProcessSteps stepName, string sourceColumn);
    }
}
