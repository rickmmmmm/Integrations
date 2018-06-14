using System;
using System.Collections.Generic;
using System.Linq;

namespace MiddleWay_Utilities
{
    public class ProcessInput
    {
        #region Variables and Properties

        private List<string> _parameters;

        #endregion Variables and Properties

        #region Constructor

        public ProcessInput(List<string> parameters)
        {
            _parameters = parameters;
        }

        #endregion Constructor

        #region Functions

        /// <summary>
        /// Get all options (single dash) in the parameters
        /// </summary>
        /// <returns></returns>
        public List<string> ReadOptions()
        {
            var data = (from options in _parameters
                        where options.StartsWith("-") &&
                              !options.StartsWith("--")
                        select options).ToList();

            return data;
        }

        /// <summary>
        /// Check if the provider parameter is contained in the input
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool HasParameter(string parameter)
        {
            if (!parameter.StartsWith("--"))
            {
                parameter = "--" + parameter;
            }

            var data = (from options in _parameters
                        where options == parameter
                        select options).Count();

            return data == 1;
        }

        /// <summary>
        /// Return the first values associated with the specified parameter.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public string ReadParameterValue(string parameter)
        {
            if (!parameter.StartsWith("--"))
            {
                parameter = "--" + parameter;
            }

            var indexOfParam = _parameters.IndexOf(parameter);
            var value = string.Empty;
            if (indexOfParam >= 0)
            {
                var startIndex = indexOfParam + 1;

                if (!_parameters[startIndex].StartsWith("-") && !_parameters[startIndex].StartsWith("--"))
                {
                    value = _parameters[startIndex];
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
        public List<string> ReadParameterValues(string parameter)
        {
            if (!parameter.StartsWith("--"))
            {
                parameter = "--" + parameter;
            }

            var indexOfParam = _parameters.IndexOf(parameter);
            var values = new List<string>();
            if (indexOfParam >= 0)
            {
                for (var startIndex = indexOfParam + 1; startIndex < _parameters.Count(); startIndex++)
                {
                    if (_parameters[startIndex].StartsWith("-") || _parameters[startIndex].StartsWith("--"))
                    {
                        break;
                    }
                    else
                    {
                        values.Add(_parameters[startIndex]);
                    }
                }
            }
            return values;

        }

        #endregion Functions
    }
}
