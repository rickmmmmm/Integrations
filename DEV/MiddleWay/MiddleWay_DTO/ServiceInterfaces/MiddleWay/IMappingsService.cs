using MiddleWay_DTO.Enumerations;
using System.Collections.Generic;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay
{
    public interface IMappingsService
    {
        bool HasMappings(ProcessSteps stepName);
        U Map<T, U>(T item, ProcessSteps stepName) where U : new();
        List<U> Map<T, U>(List<T> item, ProcessSteps stepName) where U : new();
    }
}
