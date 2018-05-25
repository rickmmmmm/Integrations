using System;
using System.Collections.Generic;
using System.Text;
using MiddleWay_DTO.Enumerations;

namespace MiddleWay_DTO.Models
{

    public class DbErrorEventArgs : EventArgs
    {
        public string InterfaceMessage { get; set; }
        public string ExceptionMessage { get; set; }
    }

    public class DbActivityEventArgs : EventArgs
    {
        public string ActivityStep { get; set; }
        public string ActivityMessage { get; set; }
    }

    public class ErrorEventArgs : EventArgs
    {
        public string message { get; set; }
        public string actionName { get; set; }
        public ChangeTypeEnum type { get; set; }
        public ErrorData Data { get; set; }
    }

    public class ErrorData
    {
        public string Reference { get; set; }
        public string Reason { get; set; }
        public string ExceptionMessage { get; set; }
        public string RejectedValue { get; set; }
        public int LineNumber { get; set; }
    }
}
