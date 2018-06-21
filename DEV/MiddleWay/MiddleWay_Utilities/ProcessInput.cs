using System;
using System.Collections.Generic;
using System.Linq;

namespace MiddleWay_Utilities
{
    public static class ProcessInput
    {
        #region Variables and Properties

        //private List<string> _parameters;

        #endregion Variables and Properties

        //#region Constructor

        //public ProcessInput(List<string> parameters)
        //{
        //    _parameters = parameters;
        //}

        //#endregion Constructor

        #region Functions

        /// <summary>
        /// Get all options (single dash) in the parameters
        /// </summary>
        /// <returns></returns>
        public static List<string> ReadOptions(List<string> options)
        {
            var data = (from parameters in options
                        where parameters.StartsWith("-") &&
                              !parameters.StartsWith("--")
                        select parameters).ToList();

            return data;
        }

        /// <summary>
        /// Check if the provider parameter is contained in the input
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static bool HasParameter(List<string> options, string parameter)
        {
            if (!parameter.StartsWith("--"))
            {
                parameter = "--" + parameter;
            }

            var data = (from parameters in options
                        where parameters == parameter
                        select parameters).Count();

            return data == 1;
        }

        /// <summary>
        /// Return the first values associated with the specified parameter.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static string ReadParameterValue(List<string> options, string parameter)
        {
            if (!parameter.StartsWith("--"))
            {
                parameter = "--" + parameter;
            }

            var indexOfParam = options.IndexOf(parameter);
            var value = string.Empty;
            if (indexOfParam >= 0)
            {
                var startIndex = indexOfParam + 1;

                if (!options[startIndex].StartsWith("-") && !options[startIndex].StartsWith("--"))
                {
                    value = options[startIndex];
                }
            }
            return value;

        }

        /// <summary>
        /// Return all values associated with the specified parameter. All values
        /// before the next option or parameter
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static List<string> ReadParameterValues(List<string> options, string parameter)
        {
            if (!parameter.StartsWith("--"))
            {
                parameter = "--" + parameter;
            }

            var indexOfParam = options.IndexOf(parameter);
            var values = new List<string>();
            if (indexOfParam >= 0)
            {
                for (var startIndex = indexOfParam + 1; startIndex < options.Count(); startIndex++)
                {
                    if (options[startIndex].StartsWith("-") || options[startIndex].StartsWith("--"))
                    {
                        break;
                    }
                    else
                    {
                        values.Add(options[startIndex]);
                    }
                }
            }
            return values;

        }

        #endregion Functions
    }
}
