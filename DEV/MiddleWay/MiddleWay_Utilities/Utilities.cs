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


        /// <summary>
        /// Gets whether or not the <i>expression</i> is numeric
        /// by checking each character to see if it is a number.
        /// http://aspalliance.com/80_Benchmarking_IsNumeric_Options.all
        /// </summary>
        /// <param name="expression">Value to check.</param>
        /// <returns>Whether or not the <i>expression</i> is numeric
        /// by checking each character to see if it is a number.</returns>
        public static bool IsANumber(string expression)
        {
            bool hasDecimal = false;

            for (int i = 0; i < expression.Length; i++)
            {
                // Check for negative symbol not in first character
                if (expression[i] == '-' && i != 0)
                {
                    return false;
                }
                else
                {
                    // Check for decimal
                    if (expression[i] == '.')
                    {
                        if (hasDecimal) // 2nd decimal
                        {
                            return false;
                        }
                        else // 1st decimal
                        {
                            // inform loop decimal found and continue 
                            hasDecimal = true;
                            continue;
                        }
                    }

                    // check if number
                    if (!char.IsNumber(expression[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
