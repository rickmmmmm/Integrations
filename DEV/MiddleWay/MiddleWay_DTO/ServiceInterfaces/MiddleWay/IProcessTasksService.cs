using MiddleWay_DTO.Models.MiddleWay;
using System.Collections.Generic;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay
{
    public interface IProcessTasksService
    {
        int GetProcessTaskUid { get; }

        ProcessTasksModel GetProcessTaskByProcessUid(int processTaskUid);

        List<ProcessTasksModel> GetProcessTasks(int processUid);

        List<ProcessTasksModel> GetProcessTasks(string client, string processName);

        ProcessTasksModel GetLatestProcessTask(int processUid);

        ProcessTasksModel GetLatestProcessTask(string client, string processName);

        int StartProcessTask(string parameters = null);

        void EndProcessTask(bool success = false);
    }
}
