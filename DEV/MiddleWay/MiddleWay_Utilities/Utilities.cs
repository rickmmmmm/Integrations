﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

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

            var res = CleanupHiddenCharacters(result.ToString());

            return res;

        }

        public static string CleanupHiddenCharacters(string data)
        {
            //NewLine, Carriage return, tabs to spaces, other white space cleanup
            string result = data;
            result = result.Replace(System.Environment.NewLine, "\\r\\n");
            result = result.Replace(((char)13).ToString(), "\\r");
            result = result.Replace(((char)12).ToString(), "\\f");
            result = result.Replace(((char)11).ToString(), "\\v");
            result = result.Replace(((char)10).ToString(), "\\n");
            result = result.Replace(((char)9).ToString(), "    ");

            return result;
        }

        //Cleanup phone number (remove formatting)

        //Clean Zip codes (remove formatting)

        //Standarize dates...Nice to have

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
                if (expression[i] == '-')
                {
                    if (i != 0)
                    {
                        return false;
                    }
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

        public static string ToStringObject<T>(T input)
        {
            //Iterate through the properties and print each value and property name
            var output = new StringBuilder();

            if (input is System.Dynamic.ExpandoObject)
            {
                output.AppendLine(Utilities.ToStringDynamic(input as IDictionary<string, object>));
            }
            else
            {
                var enumerable = input as IEnumerable;

                if (enumerable != null)
                {
                    output.AppendLine(Utilities.ToStringIEnumerable(enumerable));
                }
                else
                {
                    var properties = input.GetType().GetProperties();

                    foreach (var property in properties)
                    {
                        var val = property.GetValue(input);
                        output.AppendLine(property.Name + ":\t" + (val ?? string.Empty).ToString());
                    }
                }
            }

            return output.ToString();
        }

        private static string ToStringDynamic(IDictionary<string, object> input)
        {
            //Iterate through the elements in the dictionary and print each value and property name
            var output = new StringBuilder();

            foreach (var row in input)
            {
                output.AppendLine(row.Key + ":\t" + (row.Value ?? string.Empty).ToString());
            }

            return output.ToString();
        }

        private static string ToStringIEnumerable(IEnumerable enumerable)
        {
            // for each item in the enumerable iterate and toString each item
            var output = new StringBuilder();

            foreach (var value in enumerable)
            {
                return Utilities.ToStringObject(value);
            }

            return output.ToString();
        }
    }
}
