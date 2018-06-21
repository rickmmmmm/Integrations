using MiddleWay_DTO.Models.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.RepositoryInterfaces.MiddleWay
{
    public interface IProcessesRepository
    {
        ProcessesModel SelectProcess(int processUid);
        ProcessesModel SelectProcess(string client, string processName);
        int SelectProcessUid(string client, string processName);
    }
}
