using MiddleWay_DTO.Enumerations;
using MiddleWay_DTO.Models.MiddleWay_Controller;
using System.Collections.Generic;

namespace MiddleWay_DTO.RepositoryInterfaces.MiddleWay
{
    public interface ITransformationsRepository
    {
        bool HasTransformations(int processUid, ProcessSteps stepName);
        bool HasTransformations(string client, string processName, ProcessSteps stepName);
        List<TransformationsModel> SelectTransformations(int processUid, ProcessSteps stepName);
        List<TransformationsModel> SelectTransformations(string client, string processName, ProcessSteps stepName);
    }
}
