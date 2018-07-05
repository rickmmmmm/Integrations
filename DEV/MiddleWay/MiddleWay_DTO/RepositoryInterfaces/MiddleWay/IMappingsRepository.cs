using MiddleWay_DTO.Models.MiddleWay;
using System.Collections.Generic;

namespace MiddleWay_DTO.RepositoryInterfaces.MiddleWay
{
    public interface IMappingsRepository
    {
        List<MappingsModel> SelectMappings(int processUid);
        List<MappingsModel> SelectMappings(string client, string processName);
        MappingsModel SelectMappings(int processUid, string sourceColumn);
        MappingsModel SelectMappings(string client, string processName, string sourceColumn);
    }
}
