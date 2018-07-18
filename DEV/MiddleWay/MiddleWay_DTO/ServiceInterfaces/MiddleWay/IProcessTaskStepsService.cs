using MiddleWay_DTO.Enumerations;
using MiddleWay_DTO.Models.MiddleWay;
using System.Collections.Generic;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay
{
    public interface IProcessTaskStepsService
    {
        ProcessTaskStepsModel GetTaskStep(int processTaskStepUid);
        List<ProcessTaskStepsModel> GetTaskSteps(int processTaskUid);
        ProcessTaskStepsModel GetLatestTaskStep(string client, string processName, ProcessSteps stepName);
        List<ProcessTaskStepsModel> GetLatestProcessTaskSteps(int processUid);
        List<ProcessTaskStepsModel> GetLatestProcessTaskSteps(string client, string processName);
        int BeginTaskStep(int processTaskUid, ProcessSteps stepName);
        bool EndTaskStep(int processTaskStepUid, bool success);
        bool EndTaskStep(int processTaskUid, ProcessSteps stepName, bool success);
    }
}
