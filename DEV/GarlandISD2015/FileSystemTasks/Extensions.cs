using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemTasks
{
    public static class Extensions
    {
        public static DateTime ToDateTimeFromString(this string dateString)
        {
            return new DateTime(Convert.ToInt32(dateString.Substring(0, 4)), Convert.ToInt32(dateString.Substring(4, 2)), Convert.ToInt32(dateString.Substring(6, 2)));
        }

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
    }
}
