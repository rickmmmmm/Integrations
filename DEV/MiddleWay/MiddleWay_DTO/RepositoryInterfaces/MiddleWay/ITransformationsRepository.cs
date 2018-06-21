using MiddleWay_DTO.Models.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.RepositoryInterfaces.MiddleWay
{
    public interface ITransformationsRepository
    {
        List<TransformationsModel> SelectTransformations(int processUid);

        List<TransformationsModel> SelectTransformations(string client, string processName);
    }
}
