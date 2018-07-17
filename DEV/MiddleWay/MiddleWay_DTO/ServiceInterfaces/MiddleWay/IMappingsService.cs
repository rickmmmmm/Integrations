using System.Collections.Generic;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay
{
    public interface IMappingsService
    {
        U Map<T, U>(T item, string stepName) where U : new();
        List<U> Map<T, U>(List<T> item, string stepName) where U : new();
    }
}
