using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemTasks
{
    public static class Extensions
    {
        public static DateTime ToDateTimeFromString(this string dateString)
        {
            return new DateTime(Convert.ToInt32(dateString.Substring(0, 4)), Convert.ToInt32(dateString.Substring(4, 2)), Convert.ToInt32(dateString.Substring(6, 2)));
        }

        /// <summary>
        /// Tests whether a date string like yyyymmdd can be converted to a DateTime object.
        /// </summary>
        /// <param name="dateString">String to convert.</param>
        /// <returns></returns>
        public static bool IsValidDateFromString(this string dateString)
        {
            try
            {
                DateTime outDate = new DateTime(Convert.ToInt32(dateString.Substring(0, 4)), Convert.ToInt32(dateString.Substring(4, 2)), Convert.ToInt32(dateString.Substring(6, 2)));

                return true;
            }

            catch
            {
                return false;
            }
        }

        public static bool IsValidDateFromString(this string dateString, bool preFormatted)
        {
            try
            {
                DateTime outDate = Convert.ToDateTime(dateString);

                return true;
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Calls string.isNullOrEmpty() to see if the mapping value is not an empty string. This ensures that the parser will not try to read non-existent columns.
        /// </summary>
        /// <param name="mapValue">String to test.</param>
        /// <returns></returns>
        public static bool IsValidMap(this string mapValue)
        {
            if (string.IsNullOrEmpty(mapValue))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// If string exceeds specified number of characters, truncates string to number of characters specified.
        /// </summary>
        /// <param name="value">String to potentially truncate.</param>
        /// <param name="maxChars">Specified max length of string. If "value" length exceeds this number, output will be truncated to this length.</param>
        /// <returns></returns>
        public static string Truncate(this string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars - 1);
        }

        /// <summary>
        /// Mesa-specific extension method to correctly parse decimal values from their file.
        /// </summary>
        /// <param name="chargeAmount"></param>
        /// <param name="decimalLength"></param>
        /// <returns></returns>
        public static decimal MesaConvertChargeAmount(this string chargeAmount, int decimalLength)
        {
            string decimalPortion = chargeAmount.Substring(chargeAmount.Length - (decimalLength + 1), decimalLength);
            string nonDecimalPortion = chargeAmount.Substring(0, chargeAmount.Length - decimalLength);

            decimal outputValue = Convert.ToDecimal(nonDecimalPortion + "." + decimalPortion);

            return outputValue;
        }
    }
}
