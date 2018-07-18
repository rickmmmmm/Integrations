using MiddleWay_DTO.Enumerations;
using MiddleWay_DTO.Models.MiddleWay;
using System;
using System.Collections.Generic;

namespace MiddleWay_DTO.RepositoryInterfaces.MiddleWay
{
    public interface IProcessTaskStepsRepository
    {
        ProcessTaskStepsModel SelectProcessTaskStep(int processTaskStepUid);
        ProcessTaskStepsModel SelectProcessTaskStep(int processTaskUid, ProcessSteps stepName);
        List<ProcessTaskStepsModel> SelectProcessTaskSteps(int processTaskUid);
        ProcessTaskStepsModel SelectLatestProcessTaskStep(string client, string processName, ProcessSteps stepName);
        List<ProcessTaskStepsModel> SelectLatestProcessTaskSteps(int processUid);
        List<ProcessTaskStepsModel> SelectLatestProcessTaskSteps(string client, string processName);
        int InsertProcessTaskStep(ProcessTaskStepsModel processTaskStep);
        int InsertProcessTaskStep(int processTaskUid, ProcessSteps stepName);
        bool UpdateProcessTaskStep(ProcessTaskStepsModel processTaskStep);
        bool UpdateProcessTaskStep(int processTaskStepsUid, DateTime endDate, bool success);
    }
}
