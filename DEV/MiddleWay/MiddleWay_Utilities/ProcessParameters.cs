using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiddleWay_Utilities
{
    public static class ProcessParameters
    {
        public static List<string> SplitParameters(string parameters)
        {
            try
            {
                //Split the string by the separator combination ][
                var splitParameters = parameters.Split("][", StringSplitOptions.RemoveEmptyEntries);

                if (splitParameters.Length > 0)
                {
                    //Remove [ from the first item
                    if (splitParameters[0][0] == '[')
                    {
                        splitParameters[0] = splitParameters[0].Substring(1);
                    }

                    //Remove ] from the last item
                    var lastItem = splitParameters[splitParameters.Length - 1];
                    var lenghtOfLast = lastItem.Length;
                    if (lastItem[lenghtOfLast - 1] == ']')
                    {
                        splitParameters[splitParameters.Length - 1] = lastItem.Substring(0, lenghtOfLast - 1);
                    }

                    return splitParameters.ToList();
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
