using System;

namespace AASTHA2.Common.Helpers
{
    public static class ValidateHelper
    {
        public static bool IsValidDate(object value)
        {
            DateTime date;
            return DateTime.TryParse(Convert.ToString(value), out date);
        }
        public static bool IsValidTime(object value)
        {
            TimeSpan offset;
            return TimeSpan.TryParse(Convert.ToString(value), out offset);
        }
        public static bool IsValidNumber(object value)
        {
            long number;
            return long.TryParse(Convert.ToString(value), out number);
        }
        //public static bool IsDefaultDate(DateTime value)
        //{
        //    DateTime date;
        //    return DateTime.TryParse(value.ToString(), out date);
        //}
    }
}
