using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay
{
    public interface ITransformationsService
    {
        //List<U> Transform<T, U>(List<T> items) where U : new();
        List<dynamic> Transform<T>(List<T> items);
        U ApplyTransformation<T, U>(T inputEntity, U outputEntity, string function, string parameters, T value);
        //U QuickCast<T, U>(T value);
        T QuickCast<T>(string value);
        T QuickCast<T>(int value);
        U Default<T, U>(T value, List<string> parameters);
        string Lookup<T>(T value, List<string> parameters);
        string Split<T>(T value, List<string> parameters);
        string Truncate<T>(T value, List<string> parameters);
        //U Cast<T, U>(T value, List<string> parameters);
        int RoundDown<T>(T value);
        int RoundUp<T>(T value);
    }
}
