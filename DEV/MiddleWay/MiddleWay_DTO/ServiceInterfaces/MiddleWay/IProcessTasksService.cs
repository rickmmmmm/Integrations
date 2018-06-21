using MiddleWay_DTO.Models.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay
{
    public interface IProcessTasksService
    {
        ProcessTasksModel GetProcessTaskByProcessUid(int processTaskUid);

        List<ProcessTasksModel> GetProcessTasks(int processUid);

        List<ProcessTasksModel> GetProcessTasks(string client, string processName);

        ProcessTasksModel GetLatestProcessTask(int processUid);

        ProcessTasksModel GetLatestProcessTask(string client, string processName);

        bool StartProcessTask(string parameters = null);

        void EndProcessTask(bool success = false);
    }
}
