using System;

namespace AASTHA2.Common.Helpers
{
    public static class ValidateHelper
    {
        public static bool IsValidDate(DateTime value)
        {
            return DateTime.TryParse(value.ToString(), out DateTime date);
        }
        public static bool IsValidTime(TimeSpan value)
        {
            return TimeSpan.TryParse(Convert.ToString(value), out TimeSpan offset);
        }
        public static bool IsValidNumber(object value)
        {
            return long.TryParse(Convert.ToString(value), out long number);
        }
        //public static bool IsDefaultDate(DateTime value)
        //{
        //    DateTime date;
        //    return DateTime.TryParse(value.ToString(), out date);
        //}
    }
}
