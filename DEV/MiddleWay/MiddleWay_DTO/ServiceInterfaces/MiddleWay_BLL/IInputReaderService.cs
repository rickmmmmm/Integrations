using System.Collections.Generic;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay_BLL
{
    public interface IInputReaderService
    {
        bool HasNext(int total);
        int GetCount();
        List<T> ReadInput<T>();
        List<T> ReadNext<T>();
        List<T> Read<T>(int offset, int limit);
    }
}
