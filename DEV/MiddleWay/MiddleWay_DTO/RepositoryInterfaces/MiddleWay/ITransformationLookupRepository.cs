using MiddleWay_DTO.Models.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.RepositoryInterfaces.MiddleWay
{
    public interface ITransformationLookupRepository
    {
        TransformationLookupModel SelectTransformationLookup(int transformationLookupUid);
        string SelectTransformationLookupValue(int transformationLookupUid);
        TransformationLookupModel SelectTransformationLookup(int processUid, string transformationLookupKey, string key);
        string SelectTransformationLookupValue(int processUid, string transformationLookupKey, string key);
        TransformationLookupModel SelectTransformationLookup(string client, string processName, string transformationLookupKey, string key);
        string SelectTransformationLookupValue(string client, string processName, string transformationLookupKey, string key);
        List<TransformationLookupModel> SelectTransformationLookups(int processUid, string transformationLookupKey);
        List<TransformationLookupModel> SelectTransformationLookups(string client, string processName, string transformationLookupKey);
    }
}
