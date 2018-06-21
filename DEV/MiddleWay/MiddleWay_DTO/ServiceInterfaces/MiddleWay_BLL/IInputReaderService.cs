using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay_BLL
{
    public interface IInputReaderService
    {
        int GetCount();
        List<T> ReadInput<T>();
        List<T> ReadNext<T>();
        List<T> Read<T>(int offset, int limit);
    }
}
