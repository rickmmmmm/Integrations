using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Utilities
{
    public static class Utilities
    {
        public static string ParseException(Exception ex)
        {
            var result = new StringBuilder();
            //var message = "";
            //    var stack = "";
            result.Append(ex.Message);
            result.AppendLine(ex.StackTrace);
            if (ex.InnerException != null)
            {
                Exception innerEx;
                innerEx = ex.InnerException;
                while (innerEx != null)
                {
                    result.Append(innerEx.Message);
                    result.AppendLine(innerEx.StackTrace);
                    innerEx = innerEx.InnerException;
                }
            }

            return result.ToString();

        }

    }
}
