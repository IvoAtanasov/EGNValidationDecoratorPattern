namespace EGNValidationDecoratorPattern
{
    using System;

    public static class ExtensionMethod
    {
        /// <summary>
        /// Convert month number into string text date
        /// Example 720 => 60 години и 10 месеца
        /// </summary>
        /// <param name="value">number of months</param>
        /// <returns>string date representation of year and months</returns>
        public static string FromMonthsToTextDate(this int value)
        {
            int years = value / 12; //div operation
            int months = value % 12; //mod operation
            string yearText = "години";
            string monthText = "месеца";
            if (years == 1)
            {
                yearText = "година";
            }
            if (months == 1)
            {
                monthText = "месец";
            }
            return String.Format("{0} {1} и {2} {3}", years, yearText, months, monthText);
        }
    }
}

