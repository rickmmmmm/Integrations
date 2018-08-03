using MiddleWay_DTO.Models.MiddleWay_Controller;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay
{
    public interface ITransformationLookupService
    {
        TransformationLookupModel GetTransformationLookup(int transformationLookupUid);
        string GetTransformationLookupValue(int transformationLookupUid);
        TransformationLookupModel GetTransformationLookup(int processUid, string transformationLookupKey, string key);
        string GetTransformationLookupValue(int processUid, string transformationLookupKey, string key, bool keepKeyValue = false);
        TransformationLookupModel GetTransformationLookup(string transformationLookupKey, string key);
        string GetTransformationLookupValue(string transformationLookupKey, string key, bool keepKeyValue = false);
        List<TransformationLookupModel> GetTransformationLookupData(string transformationLookupKey);
        List<TransformationLookupModel> GetTransformationLookupData(int processUid, string transformationLookupKey);
    }
}
