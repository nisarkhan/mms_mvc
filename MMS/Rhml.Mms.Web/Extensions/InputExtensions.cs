using System;
using System.Linq;

namespace Rhml.Mms.Web.Extensions
{
    public static class InputExtensions
    {

        /// <summary>
        /// Returns true if value is null or equal to DateTime.MinValue.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrMinValue(this System.DateTime? value)
        {
            return (value == null || value == DateTime.MinValue);
        }

        /// <summary>
        /// Returns DateTime.ToShortDateString() or string.Empty if null.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ShortDateStringFormat(this System.DateTime? value)
        {
            if (value == null) return string.Empty;
            return value.Value.ToShortDateString();
        }

        /// <summary>
        /// Returns "Yes" if true, "No" if false or null.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string BooleanToYesNo(this Boolean? value)
        {
            return (value ?? false) ? "Yes" : "No";
        }

        /// <summary>
        /// Returns true if the string is "Yes" non-case sensitive.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Boolean YesNoToBoolean(this string value)
        {
            return string.Equals(value, "yes", StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Returns the four digit fiscal year defined as current year from January to June and current year +1 July to December.
        /// Returns "----" if null.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetFiscalYear(this DateTime? value)
        {
            if (value.HasValue)
            {
                if (value.Value.Month < 7)
                {
                    return value.Value.Year.ToString();
                }
                else
                {
                    return (value.Value.Year + 1).ToString();
                }
            }
            else
            {
                return "----";
            }
        }
    }
}