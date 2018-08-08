using MiddleWay_DTO.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay
{
    public interface IProcessesService
    {
        int GetProcessUid();
        int GetProcessUid(string client, string processName);
        ProcessSources GetProcessSource();
        ProcessSources GetProcessSource(string client, string processName);
    }
}
