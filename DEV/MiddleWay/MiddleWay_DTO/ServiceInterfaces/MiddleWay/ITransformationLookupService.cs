using MiddleWay_DTO.Models.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay
{
    public interface ITransformationLookupService
    {
        List<TransformationLookupModel> GetTransformationLookupData(string transformationLookupKey);

        string LookupValue(string transformationLookupKey, string key);
    }
}
