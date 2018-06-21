using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay
{
    public interface IProcessesService
    {
        int GetProcessUid(string client, string processName);
    }
}
