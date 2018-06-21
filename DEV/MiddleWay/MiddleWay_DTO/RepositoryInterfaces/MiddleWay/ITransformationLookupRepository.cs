using MiddleWay_DTO.Models.MiddleWay;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.RepositoryInterfaces.MiddleWay
{
    public interface ITransformationLookupRepository
    {
        TransformationLookupModel SelectTransformationLookup(int transformationLookupUid);
        List<TransformationLookupModel> SelectTransformationLookups(int processUid, string transformationLookupKey);
        List<TransformationLookupModel> SelectTransformationLookups(string client, string processName, string transformationLookupKey);
        string SelectTransformationLookup(int processUid, string transformationLookupKey, string key);
        string SelectTransformationLookup(string client, string processName, string transformationLookupKey, string key);
    }
}
