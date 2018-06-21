using MiddleWay_DTO.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_DTO.ServiceInterfaces.MiddleWay_BLL
{
    public interface IInputService
    {
        //DataSourceTypes DataSourceType { get; set; }
        //string SourcePath { get; set; }
        //string Delimiter { get; set; }
        //string TextQualifier { get; set; }
        //string Connection { get; set; }
        //string QuerySelect { get; set; }
        //string QueryBody { get; set; }
        //string QueryWhere { get; set; }
        //string QueryOffset { get; set; }
        //int OffSet { get; set; }
        //int Limit { get; set; }

        bool HasNext();

        int GetInputCount();

        List<T> ReadInput<T>() where T : new();

        List<T> ReadNext<T>() where T : new();

        List<T> Read<T>(int offset, int limit) where T : new();
    }
}
