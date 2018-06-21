using MiddleWay_DTO.Models.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.RepositoryInterfaces.MiddleWay
{
    public interface IProcessTasksRepository
    {
        //Select Task by Uid
        ProcessTasksModel SelectProcessTask(int processTaskUid);
        //Select List by ProcessUid
        List<ProcessTasksModel> SelectProcessTasks(int processUid);
        //Select List by Client, ProcessName
        List<ProcessTasksModel> SelectProcessTasks(string client, string processName);
        //Select Latest task by ProcessUid
        ProcessTasksModel SelectLatestProcessTask(int processUid);
        //Select Latest task by Client, ProcessName
        ProcessTasksModel SelectLatestProcessTask(string client, string processName);
        //Insert Task
        int InsertProcessTask(ProcessTasksModel processTask);
        //Insert Task By Client, ProcessName
        int InsertProcessTask(string client, string processName, string parameters = "");
        //Update Task
        bool UpdateProcessTask(ProcessTasksModel processTask);
        //Update Task End Date by ProcessUid
        bool UpdateProcessTask(int processTaskUid, DateTime endTime);
        //Update Task End Date and Success by ProcessUid
        bool UpdateProcessTask(int processTaskUid, DateTime endTime, bool successful = false);
    }
}
