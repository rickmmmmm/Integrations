using MiddleWay_DTO.Models.MiddleWay;
using System.Collections.Generic;

namespace MiddleWay_DTO.RepositoryInterfaces.MiddleWay
{
    public interface ITransformationsRepository
    {
        List<TransformationsModel> SelectTransformations(int processUid, string stepName);

        List<TransformationsModel> SelectTransformations(string client, string processName, string stepName);
    }
}
