using MiddleWay_DTO.Enumerations;
using System;
using System.Collections.Generic;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay
{
    public interface ITransformationsService
    {
        dynamic Transform<T>(T item, ProcessSteps stepName);
        List<dynamic> Transform<T>(List<T> items, ProcessSteps stepName);
        object ApplyTransformation(string function, string parameters, string value);
        object ApplyTransformation(string function, string parameters, int value);
        object ApplyTransformation(string function, string parameters, int? value);
        object ApplyTransformation(string function, string parameters, double value);
        object ApplyTransformation(string function, string parameters, double? value);
        object ApplyTransformation(string function, string parameters, bool value);
        object ApplyTransformation(string function, string parameters, bool? value);
        object ApplyTransformation(string function, string parameters, object value);
        U ApplyTransformation<T, U>(T inputEntity, U outputEntity, string function, string parameters, T value);
        U QuickCast<T, U>(T value);
        //void QuickCast<T, U>(T value, out U destination);
        object QuickCast<T>(T value, Type destinationType);
        T QuickCast<T>(string value);
        T QuickCast<T>(int value);
        T QuickCast<T>(double value);
        T QuickCast<T>(decimal value);
        T QuickCast<T>(bool value);
        T QuickCast<T>(DateTime value);
        U Default<T, U>(T value, List<string> parameters);
        string Lookup<T>(T value, List<string> parameters);
        string Split<T>(T value, List<string> parameters);
        string Truncate<T>(T value, List<string> parameters);
        //U Cast<T, U>(T value, List<string> parameters);
        int RoundDown<T>(T value);
        int RoundUp<T>(T value);
        //object GetOutputVariable(System.Reflection.PropertyInfo destinationProperty);
    }
}
